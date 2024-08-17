﻿namespace Paraminter.Invalidation;

using Moq;

using Paraminter.Invalidation.Commands;
using Paraminter.Invalidation.Models;
using Paraminter.Invalidation.Queries;

using System;

using Xunit;

public sealed class Handle
{
    private readonly IFixture Fixture = FixtureFactory.Create();

    [Fact]
    public void NullCommand_ThrowsArgumentNullException()
    {
        var result = Record.Exception(() => Target(null!));

        Assert.IsType<ArgumentNullException>(result);
    }

    [Fact]
    public void ValidArguments_InvalidatesModel()
    {
        Mock<IWriteOnlyArgumentAssociationsInvalidity> invalidityMock = new();

        Fixture.InvalidityProviderMock.Setup(static (handler) => handler.Handle(It.IsAny<IGetArgumentAssociationsInvalidityQuery>())).Returns(invalidityMock.Object);

        Target(Mock.Of<IInvalidateArgumentAssociationsCommand>());

        invalidityMock.Verify(static (invalidity) => invalidity.Invalidate(), Times.Once);
    }

    private void Target(
        IInvalidateArgumentAssociationsCommand command)
    {
        Fixture.Sut.Handle(command);
    }
}
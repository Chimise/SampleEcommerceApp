using Commerce.Domain.Users;
using Commerce.Domain;
using Commerce.SqlDataAccess.Aspects;
using Commerce.SqlDataAccess.Tests.Unit.Fakes;
using Commerce.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Commerce.Domain.AuditEntries;

namespace Commerce.SqlDataAccess.Tests.Unit.Aspects
{
    public class AuditingCommandServiceDecoratorTests
    {
        [Fact]
        public async Task ForwardsTheCallToTheDecoratee()
        {
            var decoratee = new SpyCommandService<object>();
            AuditingCommandServiceDecorator<object> sut =
                CreateAuditingDecorator<object>(decoratee: decoratee);

            // Act
            await sut.ExecuteAsync(command: new object());

            // Assert
            Assert.True(decoratee.ExecutedOnce);
        }

        [Fact]
        public async Task AppendsAuditEntryWithExpectedUserId()
        {
            var context = new SpyCommerceContext();
            var userContext = new StubUserContext { CurrentUserId = Guid.NewGuid() };

            AuditingCommandServiceDecorator<object> sut =
                CreateAuditingDecorator<object>(context: context, userContext: userContext);

            await sut.ExecuteAsync(command: new object());

            Assert.Equal(expected: userContext.CurrentUserId, actual: GetAppendedAuditEntry(context).UserId);

        }

        [Fact]
        public async Task AppendsAuditEntryWithExpectedTimeOfExecution()
        {
            var context = new SpyCommerceContext();
            var timeProvider = new StubTimeProvider { Now = DateTime.Parse("2025-03-05") };
            AuditingCommandServiceDecorator<object> sut 
                = CreateAuditingDecorator<object>(context: context, timeProvider: timeProvider);

            await sut.ExecuteAsync(command: new object());

            Assert.Equal(expected: timeProvider.Now, 
                actual: GetAppendedAuditEntry(context).TimeOfExecution);

        }

        private static AuditEntry GetAppendedAuditEntry(CommerceContext context)
        {
            var entries = context.ChangeTracker.Entries<AuditEntry>();
            Assert.Single(entries);
            return entries.Single().Entity;
        }

        private static AuditingCommandServiceDecorator<TCommand> CreateAuditingDecorator<TCommand>(
            IUserContext? userContext = null,
            ITimeProvider? timeProvider = null,
            CommerceContext? context = null,
            ICommandHandler<TCommand>? decoratee = null) where TCommand : class
        {
            return new AuditingCommandServiceDecorator<TCommand>(
                userContext: userContext ?? new StubUserContext(),
                timeProvider: timeProvider ?? new StubTimeProvider(),
                commerceContext: context ?? new SpyCommerceContext(),
                decoratee: decoratee ?? new StubCommandService<TCommand>());
        }
    }
}

using InfraSim.Pages.Models;
using InfraSim.Pages.Models.Commands;
using InfraSim.Pages.Models.Capabilities;
using InfraSim.Pages.Models.Database;

namespace InfraSim.Tests.CommandTests
{
    public class CommandManagerTests
    {
        private readonly ICommandManager commandManager;
        private readonly IServer testServer;
        private readonly IServerDataMapper dataMapper;
        private readonly IServerFactory factory;
        private readonly ICluster cluster;

        public CommandManagerTests()
        {
            commandManager = new CommandManager();

            var context = new MemoryInfraSimContext();
            context.Database.EnsureCreated();

            var capabilityFactory = new ServerCapabilityFactory();
            factory = new ServerFactory(capabilityFactory);
            testServer = factory.CreateServer(ServerType.Server);

            var repoFactory = new RepositoryFactory(context);
            var unitOfWork = new UnitOfWork(context, repoFactory);
            dataMapper = new ServerDataMapper(unitOfWork, capabilityFactory);

            cluster = (ICluster)factory.CreateCluster();
        }

        [Fact]
        public void Execute_AddServerCommand_ShouldAddServer()
        {
            var command = new AddServerCommand(cluster, testServer, dataMapper);
            commandManager.Execute(command);

            Assert.Contains(testServer, cluster.Servers);
        }

        [Fact]
        public void Undo_ShouldRemovePreviouslyAddedServer()
        {
            var command = new AddServerCommand(cluster, testServer, dataMapper);
            commandManager.Execute(command);
            commandManager.Undo();

            Assert.DoesNotContain(testServer, cluster.Servers);
        }

        [Fact]
        public void Redo_ShouldReAddServerAfterUndo()
        {
            var command = new AddServerCommand(cluster, testServer, dataMapper);
            commandManager.Execute(command);
            commandManager.Undo();
            commandManager.Redo();

            Assert.Contains(testServer, cluster.Servers);
        }

        [Fact]
        public void HasUndo_ShouldBeTrueAfterExecute()
        {
            var command = new AddServerCommand(cluster, testServer, dataMapper);
            commandManager.Execute(command);

            Assert.True(commandManager.HasUndo);
        }

        [Fact]
        public void HasRedo_ShouldBeFalse_IfNoUndo()
        {
            var command = new AddServerCommand(cluster, testServer, dataMapper);
            commandManager.Execute(command);

            Assert.False(commandManager.HasRedo);
        }
    }
}

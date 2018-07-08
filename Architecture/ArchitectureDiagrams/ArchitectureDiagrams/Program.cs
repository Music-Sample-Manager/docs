using Structurizr;
using Structurizr.Api;

namespace ArchitectureDiagrams
{
    class Program
    {
        static void Main(string[] args)
        {
            Workspace workspace = new Workspace("Music Sample Manager architecture", "This is a model of the architecture of the Music Sample Manager project.");
            Model model = workspace.Model;

            Person musician = model.AddPerson("Musician", "A person who wants to write/record some music.");
            Person libraryAuthor = model.AddPerson("Author", "A person who authors samples to be used by musicians.");

            SoftwareSystem consoleApp = model.AddSoftwareSystem("msm.exe", "A console application that lets users perform MSM operations.");
            musician.Uses(consoleApp, "Uses");

            SoftwareSystem desktopGUIApp = model.AddSoftwareSystem("Desktop GUI App", "A desktop application that lets uers perform MSM operations.");
            musician.Uses(desktopGUIApp, "Uses");

            SoftwareSystem dawClient = model.AddSoftwareSystem("DAW Client", "Functionality built into DAWs that allows users to perform MSM operations.");
            musician.Uses(dawClient, "Uses");

            SoftwareSystem website = model.AddSoftwareSystem("Website", "Website that allows musicians to browse packages, and authors to publish/manage packages.");
            musician.Uses(website, "Uses packages from");
            libraryAuthor.Uses(website, "Publishes to");


            SoftwareSystem serverSoftware = model.AddSoftwareSystem("Server software", "Handles submissions to the database, authentication, serving requests for packages, etc.");
            consoleApp.Uses(serverSoftware, "Communicates with");
            dawClient.Uses(serverSoftware, "Interfaces with");

            SoftwareSystem localPackageCache = model.AddSoftwareSystem("Local package cache", "Cache of downloaded packages, to be used by DAW projects.");
            consoleApp.Uses(localPackageCache, "Interfaces with");
            dawClient.Uses(localPackageCache, "Works with");



            ViewSet viewSet = workspace.Views;
            SystemContextView contextView = viewSet.CreateSystemContextView(consoleApp, "SystemContext", "An example of a System Context diagram.");
            contextView.AddAllSoftwareSystems();
            contextView.AddAllPeople();

            Styles styles = viewSet.Configuration.Styles;
            styles.Add(new ElementStyle(Tags.SoftwareSystem) { Background = "#1168bd", Color = "#ffffff" });
            styles.Add(new ElementStyle(Tags.Person) { Background = "#08427b", Color = "#ffffff", Shape = Shape.Person });

            PublishWorkspace(workspace);
        }

        private static void PublishWorkspace(Workspace workspace)
        {
            StructurizrClient structurizrClient = new StructurizrClient("a838ec71-9f59-45ba-a8c3-cace7ebb4ac8", "b08856e0-bdbc-42b0-b031-67d076489df5");
            structurizrClient.PutWorkspace(39601, workspace);
        }
    }
}
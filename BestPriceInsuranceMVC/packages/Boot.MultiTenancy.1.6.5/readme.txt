Boot.Multitenancy 1.6.5
Project: BootProject
Project site: https://bitbucket.org/rickardmagnusson/boot.multitenancy/wiki/Home 
Owner site: http://www.bootproject.se
Project owner: Rickard Magnusson

In this release:
Changed domain value to allow get paramaters. 


Setup:
Configuration are already done in web.config.
I automatically created a "boot.configuration.config" 
file in root of application,containing configuration 
for your first database. BootProject.sdf exist in App_Data folder.

Configuration:
1. Add a reference to System.Data.SqlServerCe.dll.
2. Open Startup.cs and add a reference to Boot.Multitenancy and add line "Host.Init();" See below. 

That's all. Run your application. If all went right,
you should have a connection to your database.
Consult the project website for more information.

StartUp.cs:
using Boot.Multitenancy;
namespace ....
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Host.Init(); //<!-------------Init Boot.Multitenancy-->
            ConfigureAuth(app);
        }
    }
}


Example model:
using Boot.Multitenancy.Intrastructure.Domain;


namespace ...
{
    public class Content : IEntity
    {
	    public virtual Int32 Id { get; set; }
	    public virtual string Title { get; set; }
	    public virtual string Html { get; set; }
    }

    public class ContentMap : Entity<Content>
    {
        public ContentMap()
        {
	        Id(x => x.Id).Unique().Column("Id").Not.Nullable().GeneratedBy.Identity().CustomType<Int32>();         
	        Map(p => p.Title);
	        Map(p => p.Html);   
        }
    }
}



Example to retrieve data in model, controller etc.
using NHibernate;
using Boot.Multitenancy;
using Boot.Multitenancy.Extensions; //Boot.Multitenancy extensions.

List<Content> ListOfContent = Host.Open().All<Content>();
...


The new hierarchy feature:
----------------------------------------------------------------------------------------
This lets you create a hierarky of any object that inherits Node class.
Lets say you want to create a simple UL LI list. This is very easy with this class.
Let me show you an example.

Create a model SiteNode:

using Boot.Multitenancy.Intrastructure.Domain; <!--Important-->
namespace......
    public class SiteNode : Node, IEntity //<!----Inherit Node class
    {
        public virtual string Text { get; set; }
        public virtual string Url { get; set; }
    }
    public class SiteNodeMap : Entity<SiteNode>
    {
        public SiteNodeMap()
        {
	    //Id and ParentId is inherited from base class so dont add these in SiteNode class.
            Id(x => x.Id)
               .Column("Id") 
               .GeneratedBy.Assigned()
               .CustomType<Int32>();
            Map(p => p.ParentId);

            Map(p => p.Text);
            Map(p => p.Url);
        }
    } 
	//For simplicity I added this code in HomeController, but its better to have a viewmodel, repo...
 	public ActionResult Index()
        {
            var treeData = Host.Open().All<SiteNode>()
				.ToBaseList() //Prepare SiteNodes for hierarchy
                    		.ToHierarchy() //Extension that Adds a Parent Child relation
                        	.TreeView(c => c.Nodes, c => c.Data.Text, c=> c.Data.Url); // Shouldn't exist in MultiTenancy project is here for demonstation.

            return View("Index", "_Layout", treeData);
        }

	in model just add @Html.Raw(Model)
	Start the site to create the table.
	Open the table and add some items in it.

	For eg: 1, 0, Root, /home
		2, 1, Sub 1.1, /sub1.1
		.....


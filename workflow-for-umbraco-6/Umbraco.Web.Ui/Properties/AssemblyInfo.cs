using System.Reflection;
using System.Runtime.InteropServices;
using System.Web;
using FergusonMoriyam.Workflow.Umbraco.Application;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Moriyama Workflow for Umbraco 4")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Moriyama")]
[assembly: AssemblyProduct("Moriyama Workflow for Umbraco 4")]
[assembly: AssemblyCopyright("Copyright © Moriyama 2013")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("e6fb166a-e035-45f7-99dd-38817f29eb34")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Revision and Build Numbers 
// by using the '*' as shown below:
[assembly: AssemblyVersion("2.1.0.0")]
[assembly: AssemblyFileVersion("2.1.0.0")]
[assembly: System.Web.UI.WebResource("css/grid.css", "text/css")]
[assembly: PreApplicationStartMethod(typeof(UmbracoWorkflowApplicationBase), "RegisterModules")]

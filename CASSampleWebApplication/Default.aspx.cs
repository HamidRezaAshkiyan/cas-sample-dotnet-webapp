namespace CASGrouperWebServicesWebApplication
{
    using System;
    using System.Web.UI;

    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            log4net.Config.XmlConfigurator.Configure();
        }
    }
}
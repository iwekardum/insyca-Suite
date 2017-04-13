
namespace inSyca.foundation.framework.configurator
{
    public class ApplicationContext : inSyca.foundation.framework.application.windowsforms.FrameworkApplicationContext
    {
        override protected void ShowIntroForm()
        {
            if (introForm == null)
            {
                introForm = new framework_configurator();
                introForm.Closed += mainForm_Closed; // avoid reshowing a disposed form
//                ElementHost.EnableModelessKeyboardInterop(introForm);
                introForm.Show();
            }
            else { introForm.Activate(); }
        }

        override protected void ShowDetailsForm()
        {
            if (introForm == null)
            {
                introForm = new framework_configurator();
                introForm.Closed += mainForm_Closed; // avoid reshowing a disposed form
                //                ElementHost.EnableModelessKeyboardInterop(introForm);
                introForm.Show();
            }
            else { introForm.Activate(); }
        }

    }
}

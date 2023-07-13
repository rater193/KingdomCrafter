using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ModIO;

public class TestScript : MonoBehaviour
{
    public void ClickCheckInstalledMods()
    {

        Debug.Log("Retreiving user's subscribed mods");
            
        SubscribedMod[] modsSubbed = ModIOUnity.GetSubscribedMods(out Result resSubbed);
        foreach (var mod in modsSubbed)
        {
            Debug.Log(mod.modProfile.name + 
                      ", Installed: " + ((mod.status==SubscribedModStatus.Installed) ? "Yes" : "No") +
                      ", Enabled: " + ((mod.enabled) ? "Yes" : "No") +
                      ", Directory: " + mod.directory
            );
        }
        
        Debug.Log("Retreiving user's installed mods");
        
        UserInstalledMod[] modsInstalled = ModIOUnity.GetInstalledModsForUser(out Result resInstalled, true);
        foreach (var mod in modsInstalled)
        {
            Debug.Log(mod.modProfile.name +
                      ", Directory: " + mod.directory
                      );
        }
    }
    
    public void Open()
    {
        ModIOBrowser.Browser.Open(OnDoneBrowsing);
    }

    public void OnDoneBrowsing()
    {
        Debug.Log("Finished browsing mods?");
    }
}

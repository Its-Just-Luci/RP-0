using RP0.ProceduralAvionics;
using System.Collections;
using UnityEngine;

namespace RP0.Avionics
{
    [KSPAddon(KSPAddon.Startup.EditorAny, false)]
    public class EditorBinder : MonoBehaviour
    {
        public void Start()
        {
            //GameEvents.onEditorPartEvent.Add(OnEditorPartEvent);
            GameEvents.onPartActionUIShown.Add(OnPartActionUIShown);
            GameEvents.onPartActionUIDismiss.Add(OnPartActionUIDismiss);
        }

        public void OnDestroy()
        {
            //GameEvents.onEditorPartEvent.Remove(OnEditorPartEvent);
            GameEvents.onPartActionUIShown.Remove(OnPartActionUIShown);
            GameEvents.onPartActionUIDismiss.Remove(OnPartActionUIDismiss);
        }

        private void OnEditorPartEvent(ConstructionEventType type, Part part)
        {
            if (IsAttached(type, part) || IsCreatedAsRoot(type, part))
            {
                StartCoroutine(OpenAvionicsUIRoutine(part));
            }
        }

        private static bool IsAttached(ConstructionEventType type, Part part)
        {
            return type == ConstructionEventType.PartAttached &&
                   part.Modules.Contains(nameof(ModuleProceduralAvionics));
        }

        private static bool IsCreatedAsRoot(ConstructionEventType type, Part part)
        {
            return type == ConstructionEventType.PartCreated &&
                   EditorLogic.RootPart == part &&
                   part.Modules.Contains(nameof(ModuleProceduralAvionics));
        }

        private void OnPartActionUIShown(UIPartActionWindow paw, Part part)
        {
            if (!part.Modules.Contains(nameof(ModuleProceduralAvionics))) return;

            var pm = (ModuleProceduralAvionics)part.Modules[nameof(ModuleProceduralAvionics)];
            if (pm != null) pm.showGUI = true;
        }

        private void OnPartActionUIDismiss(Part part)
        {
            if (!part.Modules.Contains(nameof(ModuleProceduralAvionics))) return;

            var pm = (ModuleProceduralAvionics)part.Modules[nameof(ModuleProceduralAvionics)];
            if (pm != null) pm.showGUI = false;
        }

        private IEnumerator OpenAvionicsUIRoutine(Part part)
        {
            yield return new WaitForFixedUpdate();
            yield return new WaitForFixedUpdate();

            var pm = (ModuleProceduralAvionics)part.Modules[nameof(ModuleProceduralAvionics)];
            pm.showGUI = true;
        }
    }
}

using System;
using Dalamud.Plugin.Services;

namespace AutoSelfie
{
    public static unsafe class CameraManager
    {
        [PluginService] public static IPluginLog PluginLog { get; private set; } = null!;
        [PluginService] public static IFramework Framework { get; private set; } = null!;
        [PluginService] public static Dalamud.Plugin.Services.ICameraManager CameraManagerService { get; private set; } = null!;

        public static void SetCamera(float yawDegrees, float pitchDegrees, float zoomDistance)
        {
            try
            {
                unsafe
                {
                    var camera = CameraManagerService->GetActiveCamera();
                    if (camera == null)
                        return;

                    camera->Yaw = DegreesToRadians(yawDegrees);
                    camera->Pitch = DegreesToRadians(pitchDegrees);
                    camera->Distance = zoomDistance;
                }
            }
            catch (Exception ex)
            {
                PluginLog.Error(ex, "Failed to set camera.");
            }
        }

        private static float DegreesToRadians(float degrees)
        {
            return (float)(degrees * (Math.PI / 180.0));
        }
    }
}

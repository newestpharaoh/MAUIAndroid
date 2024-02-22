using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AndroidPatientAppMaui.Utility
{
    public class Utilities
    {
        public async static Task<bool> RequestCameraAndGalleryPermissions()
        {
            try
            {
                var cameraStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
                var storageStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
                var photosStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Photos);

                if (cameraStatus != Plugin.Permissions.Abstractions.PermissionStatus.Granted || storageStatus != Plugin.Permissions.Abstractions.PermissionStatus.Granted)
                {
                    var permissionRequestResult = await CrossPermissions.Current.RequestPermissionsAsync(
                        new Permission[] { Permission.Camera, Permission.Photos });

                    var cameraResult = permissionRequestResult[Plugin.Permissions.Abstractions.Permission.Camera];
                    var photosResult = permissionRequestResult[Plugin.Permissions.Abstractions.Permission.Photos];

                    return (
                        cameraResult != Plugin.Permissions.Abstractions.PermissionStatus.Denied &&
                        photosResult != Plugin.Permissions.Abstractions.PermissionStatus.Denied);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

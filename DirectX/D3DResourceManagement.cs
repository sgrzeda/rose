using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.DirectX.Direct3D;
using System.Drawing;
using XorNet.Rose.Properties;
using XorNet.Rose.Resource;

namespace XorNet.Rose.DirectX
{
    class D3DResourceManager
    {
    }
    
    class D3DTextureManager : Common.Manager<string, D3DTexture2D>
    {
        private static D3DTextureManager _d3DTextureManagerSingleton;

        private D3DTextureManager()
        {
            
        }

        public static D3DTextureManager Instance
        {
            get { return _d3DTextureManagerSingleton ?? (_d3DTextureManagerSingleton = new D3DTextureManager()); }
        }

        protected override D3DTexture2D CreateResource(string index)
        {
            if (ResourceMap.ContainsKey(index) && ResourceMap[index].Disposed)
            {
                Logging.Error("Resource at index \"{0}\" has been disposed! Recreating.", index);
                ResourceMap.Remove(index);
                ResourceMap[index] = CreateResource(index);
                return ResourceMap[index];
			}

			string szFullPath = Path.Combine(Settings.Default.ThemeDir + '\\' + Skin.Instance.TextureDirectory, index);
			if (!File.Exists(szFullPath))
			{
				Logging.Error("The texture \"{0}\" was not found in the directory \"{1}\"", index, Settings.Default.ThemeDir + '\\' + Skin.Instance.TextureDirectory);
				return null;
			}

            return ResourceMap[index] = new D3DTexture2D(index);

        }

        public override void DeviceCreate()
        {
            base.DeviceCreate();
        }

        public override void DeviceReset()
        {
            foreach (KeyValuePair<string, D3DTexture2D> resource in ResourceMap)
            {
                resource.Value.OnDeviceReset();
            }
        }
    }
}

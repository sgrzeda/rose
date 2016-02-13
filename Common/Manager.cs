using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#if XNA
using Microsoft.Xna.Framework.Graphics;
#else
using Microsoft.DirectX.Direct3D;
#endif
namespace XorNet.Rose.Common
{
    internal class Manager<TKey, T>
    {
        protected Dictionary<TKey, T> ResourceMap;
#if XNA
        protected GraphicsDevice _graphicsDevice;
#endif // XNA
        protected Manager()
        {
            ResourceMap = new Dictionary<TKey, T>();
        }

        public T this[TKey index]
        {
            get { return GetResource(index); }
            set { ResourceMap[index] = value; }
        }

        protected virtual T GetResource(TKey index)
        {
#if XNA
            if (!CheckDevice())
                return default(T);
#endif

            if (!ResourceMap.ContainsKey(index) || ResourceMap[index] == null)
            {
                return CreateResource(index);
            }

            return ResourceMap[index];
        }

        protected virtual T CreateResource(TKey index)
        {
            Logging.Error(
                "Resource at index \"{0}\" was not found! No method for creating this resource exists! Returning null.",
                index);
            return default(T);
        }

#if XNA
        protected bool CheckDevice()
        {
            if (_graphicsDevice == null || _graphicsDevice.IsDisposed)
            {
                Logging.Error("Device does not exist in the current context.");
                return false;
            }
            return true;
        }
#endif
        public virtual void DeviceCreate(
#if XNA
            GraphicsDevice graphicsDevice
#else
            )
#endif
        {
#if XNA
            _graphicsDevice = graphicsDevice;
#endif
            ResourceMap.Clear();
        }

#if DIRECTX
        public virtual void DeviceReset()
        {
            ResourceMap.Clear();
        }
#endif
    }
}

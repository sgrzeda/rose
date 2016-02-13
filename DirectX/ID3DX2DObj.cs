using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace XorNet.Rose.DirectX
{
    interface ID3DX2DObj
    {
        /// <summary>
        /// Draws the object.
        /// </summary>
        void Draw();

        /// <summary>
        /// Handles all object updates.
        /// </summary>
        void Update();
    }
}

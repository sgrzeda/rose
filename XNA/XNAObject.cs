#if XNA
using Microsoft.Xna.Framework.Graphics;

namespace Rose.XNA
{
    internal interface IXNA2DObject
    {
        void Update();

        void Draw(SpriteBatch spriteBatch);
    }
}
#endif
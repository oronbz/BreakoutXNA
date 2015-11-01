using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Breakout.Utils
{
    public class SoundCenter
    {
        SoundEffect hit;
        SoundEffect pickupCoin;
        Game game;
        public SoundCenter(Game game)
        {
            this.game = game;
            hit = game.Content.Load<SoundEffect>("hit");
            pickupCoin = game.Content.Load<SoundEffect>("coin_pickup");
        }
        
        public SoundEffect Hit
        {
            get { return hit; }
        }
        public SoundEffect PickupCoin
        {
            get { return pickupCoin; }
        }
        
    }
}

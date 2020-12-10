using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameDevIdiotsProject1.Graphics {
    public class Animation {
        List<AnimationFrame> frames;
        TimeSpan timeIntoAnimation;
        public bool Loop { get; set; }

        // custom Duration accessor, because the length will change whenever a frame is added
        TimeSpan Duration {
            get {
                double totalSeconds = 0;
                foreach (var frame in frames) {
                    totalSeconds += frame.Duration.TotalSeconds;
                }

                return TimeSpan.FromSeconds(totalSeconds);
            }
        }

        public Rectangle CurrentRectangle {
            get {
                AnimationFrame currentFrame = null;

                // See if we can find a frame
                TimeSpan accumulatedTime = new TimeSpan();
                foreach (var frame in frames) {

                    //get whatever frame the animation should currenlty be on then break
                    if (accumulatedTime + frame.Duration >= timeIntoAnimation) {
                        currentFrame = frame;
                        break;
                    }
                    else {
                        accumulatedTime += frame.Duration;
                    }

                }

                // if no frame found, check last frame
                // in case timeIntoAnimation exceeds Duration
                if (currentFrame == null)
                    currentFrame = frames.LastOrDefault();

                // if frame was found, return rectangle - otherwise return an empty rectangle
                return (currentFrame != null) ? currentFrame.SourceRectangle : Rectangle.Empty;
            }
        }

        public void AddFrame(Rectangle rectangle, TimeSpan duration) {
            if (frames == null)
                frames = new List<AnimationFrame>();

            AnimationFrame newFrame = new AnimationFrame() {
                SourceRectangle = rectangle,
                Duration = duration
            };

            frames.Add(newFrame);
        }

        public void Update(float gameTime) {

            double secondsIntoAnimation =
                timeIntoAnimation.TotalSeconds + gameTime;

            double remainder = secondsIntoAnimation % Duration.TotalSeconds;

            timeIntoAnimation = TimeSpan.FromSeconds(remainder);
            

            
        }
    }
}

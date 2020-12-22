using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameDevIdiotsProject1.Graphics
{
	public class Animation {
        private readonly List<AnimationFrame> frames;
        private TimeSpan timeIntoAnimation;
        private float _abilityDuration;

        public Animation(float abilityDuration = 0)
        {
            _abilityDuration = abilityDuration;
            frames = new List<AnimationFrame>();
        }

        public TimeSpan Duration {
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

        public void AddFrame(Rectangle rectangle, TimeSpan duration)
        {
            //if attempting to use on an Ability Animation, redirect to Ability AddFrame
            if (_abilityDuration != 0)
            {
                AddFrame(rectangle);
                return;
            }

            AnimationFrame newFrame = new AnimationFrame()
            {
                SourceRectangle = rectangle,
                Duration = duration
            };

            frames.Add(newFrame);
        }

        public void AddFrame(Rectangle rectangle)
        {
            //default TimeSpan
            TimeSpan duration = TimeSpan.FromSeconds(.05);

            //check if this animation is linked to an ability
            if (_abilityDuration != 0)
            {
                //update frame length automatically
                duration = TimeSpan.FromSeconds((_abilityDuration/(frames.Count+1))/1000);

                //iterate through existing frames, update with new frame length
                foreach (AnimationFrame frame in frames)
                {
                    frame.Duration = duration;
                }
            }

            AnimationFrame newFrame = new AnimationFrame()
            {
                SourceRectangle = rectangle,
                Duration = duration
            };

            frames.Add(newFrame);
        }

        public void Update(float gameTime) 
        {
            double secondsIntoAnimation =
                timeIntoAnimation.TotalSeconds + gameTime;

            double remainder;
            if (secondsIntoAnimation > Duration.TotalSeconds)
                remainder = Duration.TotalSeconds;
            else
                remainder = secondsIntoAnimation % Duration.TotalSeconds;

            timeIntoAnimation = TimeSpan.FromSeconds(remainder);
        }

        public void Reset()
        {
            timeIntoAnimation = TimeSpan.Zero;
        }
    }
}

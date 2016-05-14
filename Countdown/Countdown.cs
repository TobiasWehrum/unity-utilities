using System;
using UnityEngine;

namespace UnityUtilities
{
    /// <summary>
    /// A handy countdown class providing a lot of convenience methods.
    /// </summary>
    public class Countdown
    {
        /// <summary>
        /// The time left at the current countdown.
        /// </summary>
        public float TimeLeft { get; set; }

        /// <summary>
        /// The initial duration set.
        /// </summary>
        public float Duration { get; private set; }

        /// <summary>
        /// A delegate for automatically providing a new duration
        /// when the time left runs out.
        /// </summary>
        public Func<float> CalculateDurationDelegate { get; set; }

        /// <summary>
        /// Should the countdown automatically loop when it reaches zero?
        /// </summary>
        public bool Loop { get; set; }

        /// <summary>
        /// Creates a new countdown with no TotalTime duration set yet.
        /// <param name="loop">Should the countdown loop automatically?</param>
        /// </summary>
        public Countdown(bool loop) : this(loop, 0f)
        {
        }

        /// <summary>
        /// Creates a new countdown, starting at <see cref="startDuration"/>.
        /// </summary>
        /// <param name="loop">Should the countdown loop automatically?</param>
        /// <param name="startDuration">The starting time to count down from.</param>
        public Countdown(bool loop, float startDuration)
        {
            Loop = loop;
            TimeLeft = startDuration;
            Duration = TimeLeft;
            CalculateDurationDelegate = null;
        }

        /// <summary>
        /// Creates a new countdown with a delegate specifying how long it should take.
        /// </summary>
        /// <param name="loop">Should the countdown loop automatically?</param>
        /// <param name="calculateDurationDelegate">A delegate providing the value to count down from.</param>
        /// <param name="startDuration">Optional: A one-time starting duration. Afterwards the <see cref="calculateDurationDelegate"/> is used.</param>
        public Countdown(bool loop, Func<float> calculateDurationDelegate, float? startDuration = null)
        {
            Loop = loop;

            // If there is no starting value, ask the delegate for one.
            if (startDuration == null)
                startDuration = calculateDurationDelegate();

            TimeLeft = startDuration.Value;
            Duration = startDuration.Value;
            CalculateDurationDelegate = calculateDurationDelegate;
        }

        /// <summary>
        /// Progresses the countdown by <see cref="deltaTime"/> (or Time.deltaTime). Returns true if the countdown
        /// it reached zero this frame. If the countdown is already at zero when this is called and <see cref="Loop"/>
        /// is true, it is restarted automatically.
        /// </summary>
        /// <param name="deltaTime">Optional: A deltaTime to use instead of Time.deltaTime.</param>
        /// <returns>True if the countdown reached zero this frame. False if it is still going or was already zero and isn't looping.</returns>
        public bool Progress(float? deltaTime = null)
        {
            // If no deltaTime is supplied, use Time.deltaTime
            if (deltaTime == null)
                deltaTime = Time.deltaTime;

            // If the countdown is already over, loop
            if (ReachedZero)
            {
                // ...unless we don't want to loop
                if (!Loop)
                    return false;

                // Refill the time. This might change the duration if we use a CalculateDurationDelegate.
                Refill();

                // If the duration is zero or less, this is stopped. (A CalculateDurationDelegate in
                // Refill() might have changed that, which is why we check it here and not earlier.)
                if (Duration <= 0)
                    return false;
            }

            // Progress the countdown
            TimeLeft -= deltaTime.Value;

            // Returns if the countdown reached zero
            return ReachedZero;
        }

        /// <summary>
        /// Resets the countdown to TotalTime. If a <see cref="CalculateDurationDelegate"/> is supplied,
        /// a new TotalTime is picked first.
        /// </summary>
        public void Reset()
        {
            if (CalculateDurationDelegate != null)
            {
                Duration = CalculateDurationDelegate();
            }

            TimeLeft = Duration;
        }

        /// <summary>
        /// Sets the <see cref="Duration"/> and the <see cref="TimeLeft"/>.
        /// </summary>
        /// <param name="timeLeft">The time to set TotalTime and TimeLeft to.</param>
        public void Reset(float timeLeft)
        {
            TimeLeft = timeLeft;
            Duration = timeLeft;
        }

        /// <summary>
        /// Loops the countdown by adding TotalTime to TimeLeft. This should only be called when <see cref="TimeLeft"/> is already zero
        /// or less. If a <see cref="CalculateDurationDelegate"/> is supplied, a new TotalTime is picked first.
        /// 
        /// The difference between Reset() and Loop() is that Loop() factors in when <see cref="TimeLeft"/> is already less than zero
        /// and subtracts it from the TotalTime.
        /// </summary>
        public void Refill()
        {
            if (CalculateDurationDelegate != null)
            {
                Refill(CalculateDurationDelegate());
            }
            else
            {
                Refill(Duration);
            }
        }

        /// <summary>
        /// Loops the countdown by settings TotalTime to <see cref="newTotalTimeLeft"/> and then adds it to <see cref="TimeLeft"/>.
        /// 
        /// The difference between Reset() and Loop() is that Loop() factors in when <see cref="TimeLeft"/> is already less than zero
        /// and subtracts it from the TotalTime.
        /// </summary>
        /// <param name="newTotalTimeLeft">The time to set TotalTime to and to add to TimeLeft.</param>
        public void Refill(float newTotalTimeLeft)
        {
            TimeLeft += newTotalTimeLeft;
            Duration = newTotalTimeLeft;
        }

        /// <summary>
        /// Returns true when the countdown reached zero (or less).
        /// </summary>
        public bool ReachedZero
        {
            get { return TimeLeft <= 0; }
        }

        /// <summary>
        /// Returns true if the countdown is currently over zero.
        /// </summary>
        public bool IsRunning
        {
            get { return TimeLeft > 0; }
        }

        /// <summary>
        /// Returns the time passed for this countdown: (TotalTime-TimeLeft).
        /// </summary>
        public float TimePassed
        {
            get { return Duration - TimeLeft; }
        }

        /// <summary>
        /// Returns 0 when the countdown is just starting and 1 when it is elapsed.
        /// If TotalTime is 0, this returns 1.
        /// </summary>
        public float PercentElapsed
        {
            get { return 1 - PercentLeft; }
        }

        /// <summary>
        /// Returns 1 when the countdown is just starting and 0 when it is elapsed.
        /// If TotalTime is 0, this returns 0.
        /// </summary>
        public float PercentLeft
        {
            get
            {
                if (Duration == 0)
                    return 0f;

                return Mathf.Clamp01(TimeLeft / Duration);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using UnityEngine;

namespace KModkit
{
    /// <summary>
    /// Class meant to encapsulate all types of sound effects <see cref="KMAudio"/> uses. Currently used in <see cref="ModuleBase"/>. Written by Emik.
    /// </summary>
    public sealed class Sound : IEquatable<Sound>
    {
        /// <summary>
        /// An instance of Sound where <see cref="Custom"/> is defined.
        /// </summary>
        /// <param name="sound">The sound to insert.</param>
        public Sound(string sound)
        {
            Custom = sound;
        }

        /// <summary>
        /// An instance of Sound where <see cref="Custom"/> is defined.
        /// </summary>
        /// <param name="sound">The sound to insert.</param>
        public Sound(AudioClip sound) : this(sound.name) {}

        /// <summary>
        /// An instance of sound where <see cref="Game"/> is defined.
        /// </summary>
        /// <param name="sound">The sound to insert.</param>
        public Sound(KMSoundOverride.SoundEffect sound)
        {
            Game = sound;
        }

        /// <summary>
        /// The custom sound, written out by name.
        /// </summary>
        public string Custom { get; private set; }

        /// <summary>
        /// The audio reference that is playing the sound.
        /// </summary>
        public KMAudio.KMAudioRef Reference { get; internal set; }

        /// <summary>
        /// The in-game sound.
        /// </summary>
        public KMSoundOverride.SoundEffect? Game { get; private set; }

        /// <summary>
        /// An instance of Sound where <see cref="Custom"/> is defined.
        /// </summary>
        /// <param name="sound">The sound to insert.</param>
        /// <returns><see cref="Sound"/> with argument <paramref name="sound"/>.</returns>
        public static implicit operator Sound(string sound)
        {
            return new Sound(sound);
        }

        /// <summary>
        /// An instance of Sound where <see cref="Custom"/> is defined.
        /// </summary>
        /// <param name="sound">The sound to insert.</param>
        /// <returns><see cref="Sound"/> with argument <paramref name="sound"/>.</returns>
        public static implicit operator Sound(AudioClip sound)
        {
            return new Sound(sound);
        }

        /// <summary>
        /// An instance of Sound where <see cref="Game"/> is defined.
        /// </summary>
        /// <param name="sound">The sound to insert.</param>
        /// <returns><see cref="Sound"/> with argument <paramref name="sound"/>.</returns>
        public static implicit operator Sound(KMSoundOverride.SoundEffect sound)
        {
            return new Sound(sound);
        }

        /// <summary>
        /// Returns <see cref="Custom"/> for the current variable.
        /// </summary>
        /// <param name="sound">The variable to grab the property from.</param>
        /// <returns><paramref name="sound"/>'s <see cref="Custom"/>.</returns>
        public static explicit operator string(Sound sound)
        {
            return sound.Custom;
        }

        /// <summary>
        /// Returns <see cref="Game"/> for the current variable.
        /// </summary>
        /// <param name="sound">The variable to grab the property from.</param>
        /// <returns><paramref name="sound"/>'s <see cref="Game"/>.</returns>
        public static explicit operator KMSoundOverride.SoundEffect?(Sound sound)
        {
            return sound.Game;
        }

        /// <summary>
        /// Returns <see cref="Game"/> for the current variable.
        /// </summary>
        /// <param name="sound">The variable to grab the property from.</param>
        /// <returns><paramref name="sound"/>'s <see cref="Game"/>.</returns>
        public static explicit operator KMSoundOverride.SoundEffect(Sound sound)
        {
            return sound.Game.Value;
        }

        /// <summary>
        /// Stops the <see cref="Reference"/>'s sound.
        /// </summary>
        public void StopSound()
        {
            Reference.StopSound();
        }

        /// <summary>
        /// Determines if both <see cref="Sound"/> variables are equal.
        /// </summary>
        /// <param name="obj">The comparison.</param>
        /// <returns>True if <see cref="Custom"/>, <see cref="Reference"/>, and <see cref="Game"/> are equal.</returns>
        public override bool Equals(object obj)
        {
            return Equals(obj as Sound);
        }

        /// <summary>
        /// Determines if both <see cref="Sound"/> variables are equal.
        /// </summary>
        /// <param name="other">The comparison.</param>
        /// <returns>True if <see cref="Custom"/>, <see cref="Reference"/>, and <see cref="Game"/> are equal.</returns>
        public bool Equals(Sound other)
        {
            return other == null && Custom == other.Custom && Reference == other.Reference && Game == other.Game;
        }

        /// <summary>
        /// Gets the current hash code.
        /// </summary>
        /// <returns>The hash code of <see cref="Custom"/>, <see cref="Reference"/>, and <see cref="Game"/>.</returns>
        public override int GetHashCode()
        {
            int hashCode = -675929889;
            hashCode = (hashCode * -1521134295) + EqualityComparer<string>.Default.GetHashCode(Custom);
            hashCode = (hashCode * -1521134295) + EqualityComparer<KMAudio.KMAudioRef>.Default.GetHashCode(Reference);
            hashCode = (hashCode * -1521134295) + Game.GetHashCode();
            return hashCode;
        }

        /// <summary>
        /// Converts the current sound to a string, returning the current sound.
        /// </summary>
        /// <returns><see cref="Game"/>, or if null, <see cref="Custom"/>.</returns>
        public override string ToString()
        {
            string g = Game.ToString();
            return String.IsNullOrEmpty(g) ? Custom : g;
        }
    }
}

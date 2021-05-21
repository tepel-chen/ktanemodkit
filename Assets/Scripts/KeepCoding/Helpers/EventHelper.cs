﻿using System;
using UnityEngine;
using KModkit.Internal;

namespace KModkit
{
    /// <summary>
    /// KMFramework extension methods that makes it easier to assign multiple events to a variable in one statement. Written by Emik.
    /// </summary>
    public static class EventHelper
    {
        /// <summary>
        /// Assigns events specified into <paramref name="kmSelectable"/>. Reassigning them will replace their values. The number passed into each method represents the index that came from the array.
        /// </summary>
        /// <remarks>
        /// An event that is null will be skipped. This extension method simplifies all of the KMFramework events into Actions.
        /// </remarks>
        /// <exception cref="NullIteratorException"></exception>
        /// <exception cref="EmptyIteratorException"></exception>
        /// <exception cref="UnassignedReferenceException"></exception>
        /// <param name="kmSelectable">The <see cref="KMSelectable"/> array to add events to.</param>
        /// <param name="overrideReturn">True will make it act as a module/submodule, and false as a button. Null (default) will set it to true or false based on <see cref="Helper.IsParent(KMSelectable)"/>. Note that in VR, <see cref="KMSelectable.OnHighlight"/> and <see cref="KMSelectable.OnHighlightEnded"/> are skipped out on.</param>
        /// <param name="onCancel">Called when player backs out of this selectable.</param>
        /// <param name="onDefocus">Called when a different selectable becomes the focus, or the module has been backed out of.</param>
        /// <param name="onDeselect">Called when the selectable stops being the current selectable.</param>
        /// <param name="onFocus">Called when a module is focused, this is when it is interacted with from the bomb face level and its children can be selected.</param>
        /// <param name="onHighlight">Called when the highlight is turned on.</param>
        /// <param name="onHighlightEnded">Called when the highlight is turned off.</param>
        /// <param name="onInteract">Called when player interacts with the selctable.</param>
        /// <param name="onInteractEnded">Called when a player interacting with the selectable releases the mouse or controller button.</param>
        /// <param name="onLeft">Called when the left controller stick is pulled while selected.</param>
        /// <param name="onRight">Called when the right controller stick is pulled while selected.</param>
        /// <param name="onSelect">Called whenever the selectable becomes the current selectable.</param>
        public static void Assign(this KMSelectable[] kmSelectable, bool? overrideReturn = null, Action<int> onCancel = null, Action<int> onDefocus = null, Action<int> onDeselect = null, Action<int> onFocus = null, Action<int> onHighlight = null, Action<int> onHighlightEnded = null, Action<int> onInteract = null, Action<int> onInteractEnded = null, Action<int> onLeft = null, Action<int> onRight = null, Action<int> onSelect = null)
        {
            kmSelectable.NullOrEmptyCheck("The array is not populated. Please check your public fields in Unity.");

            kmSelectable.Call((s, i) => s.Assign(
                overrideReturn,
                onCancel.ToAction(i),
                onDefocus.ToAction(i),
                onDeselect.ToAction(i),
                onFocus.ToAction(i),
                onHighlight.ToAction(i),
                onHighlightEnded.ToAction(i),
                onInteract.ToAction(i),
                onInteractEnded.ToAction(i),
                onLeft.ToAction(i),
                onRight.ToAction(i),
                onSelect.ToAction(i)));
        }

        /// <summary>
        /// Assigns events specified into <paramref name="kmSelectable"/>. Reassigning them will replace their values.
        /// </summary>
        /// <remarks>
        /// An event that is null will be skipped. This extension method simplifies all of the KMFramework events into Actions.
        /// </remarks>
        /// <exception cref="UnassignedReferenceException"></exception>
        /// <param name="kmSelectable">The KMSelectable array to add events to.</param>
        /// <param name="overrideReturn">True will make it act as a module/submodule, and false as a button. Null (default) will set it to true or false based on <see cref="Helper.IsParent(KMSelectable)"/>. Note that in VR, <see cref="KMSelectable.OnHighlight"/> and <see cref="KMSelectable.OnHighlightEnded"/> are skipped out on.</param>
        /// <param name="onCancel">Called when player backs out of this selectable. Return true will make it act as a module/submodule, and false as a button.</param>
        /// <param name="onDefocus">Called when a different selectable becomes the focus, or the module has been backed out of.</param>
        /// <param name="onDeselect">Called when the selectable stops being the current selectable.</param>
        /// <param name="onFocus">Called when a module is focused, this is when it is interacted with from the bomb face level and its children can be selected.</param>
        /// <param name="onHighlight">Called when the highlight is turned on.</param>
        /// <param name="onHighlightEnded">Called when the highlight is turned off.</param>
        /// <param name="onInteract">Called when player interacts with the selctable. Return true will make it act as a module/submodule, and false as a button.</param>
        /// <param name="onInteractEnded">Called when a player interacting with the selectable releases the mouse or controller button.</param>
        /// <param name="onLeft">Called when the left controller stick is pulled while selected.</param>
        /// <param name="onRight">Called when the right controller stick is pulled while selected.</param>
        /// <param name="onSelect">Called whenever the selectable becomes the current selectable.</param>
        public static void Assign(this KMSelectable kmSelectable, bool? overrideReturn = null, Action onCancel = null, Action onDefocus = null, Action onDeselect = null, Action onFocus = null, Action onHighlight = null, Action onHighlightEnded = null, Action onInteract = null, Action onInteractEnded = null, Action onLeft = null, Action onRight = null, Action onSelect = null)
        {
            if (kmSelectable == null)
                throw typeof(KMSelectable).Unassigned();
            if (overrideReturn == null)
                overrideReturn = kmSelectable.IsParent();

            onCancel.ToFunc(overrideReturn.Value).Set(ref kmSelectable.OnCancel);
            onDefocus.Set(ref kmSelectable.OnDefocus);
            onDeselect.Set(ref kmSelectable.OnDeselect);
            onFocus.Set(ref kmSelectable.OnFocus);
            onHighlight.Set(ref kmSelectable.OnHighlight);
            onHighlightEnded.Set(ref kmSelectable.OnHighlightEnded);
            onInteract.ToFunc(overrideReturn.Value).Set(ref kmSelectable.OnInteract);
            onInteractEnded.Set(ref kmSelectable.OnInteractEnded);
            onLeft.Set(ref kmSelectable.OnLeft);
            onRight.Set(ref kmSelectable.OnRight);
            onSelect.Set(ref kmSelectable.OnSelect);
        }

        /// <summary>
        /// Assigns events specified into <paramref name="kmGameInfo"/>. Reassigning them will replace their values.
        /// </summary>
        /// <remarks>
        /// An event that is null will be skipped. This extension method simplifies all of the KMFramework events into Actions.
        /// </remarks>
        /// <exception cref="UnassignedReferenceException"></exception>
        /// <param name="kmGameInfo">The game info instance to assign events to.</param>
        /// <param name="onAlarmClockChange">Called when the alarm clock changes state, and passes in whether it's on or off.</param>
        /// <param name="onLightsChange">Called when the lights change state, and passes in whether it's on or off.</param>
        public static void Assign(this KMGameInfo kmGameInfo, Action<bool> onAlarmClockChange = null, Action<bool> onLightsChange = null)
        {
            if (kmGameInfo == null)
                throw typeof(KMGameInfo).Unassigned();

            onAlarmClockChange.Set(ref kmGameInfo.OnAlarmClockChange);
            onLightsChange.Set(ref kmGameInfo.OnLightsChange);
        }

        /// <summary>
        /// Assigns events specified into <paramref name="kmBombInfo"/>. Reassigning them will replace their values.
        /// </summary>
        /// <remarks>
        /// An event that is null will be skipped. This extension method simplifies all of the KMFramework events into Actions or Functions.
        /// </remarks>
        /// <exception cref="UnassignedReferenceException"></exception>
        /// <param name="kmBombInfo">The bomb info instance to assign events to.</param>
        /// <param name="onBombExploded">Called when the bomb explodes.</param>
        /// <param name="onBombSolved">Called when the bomb is defused.</param>
        public static void Assign(this KMBombInfo kmBombInfo, Action onBombExploded = null, Action onBombSolved = null)
        {
            if (kmBombInfo == null)
                throw typeof(KMBombInfo).Unassigned();

            onBombExploded.Set(ref kmBombInfo.OnBombExploded);
            onBombSolved.Set(ref kmBombInfo.OnBombSolved);
        }

        /// <summary>
        /// Stops all sounds for the entire <see cref="KMAudioRef"/> <see cref="Array"/>.
        /// </summary>
        /// <param name="audioRefs">The <see cref="KMAudioRef"/> <see cref="Array"/> to mute all sounds from, using <see cref="KMAudioRef.StopSound"/>.</param>
        public static void StopSound(this KMAudio.KMAudioRef[] audioRefs)
        {
            audioRefs.ForEach(a => a.StopSound());
        }

        /// <summary>
        /// Stops all sounds for the entire <see cref="Sound"/> <see cref="Array"/>.
        /// </summary>
        /// <param name="sounds">The <see cref="Sound"/> <see cref="Array"/> to mute all sounds from, using <see cref="KMAudioRef.StopSound"/>.</param>
        public static void StopSound(this Sound[] sounds)
        {
            sounds.ForEach(s => s.StopSound());
        }

        private static Action ToAction(this Action<int> action, int i)
        {
            if (action == null)
                return null;
            return () => action(i);
        }

        private static Func<bool> ToFunc(this Action action, bool b)
        {
            if (action == null)
                return null;
            return () =>
            {
                action();
                return b;
            };
        }

        private static UnassignedReferenceException Unassigned(this Type type)
        {
            throw new UnassignedReferenceException(String.Format("The {0} is null. You cannot assign events to a {0} without a reference to a {0}.", type.Name));
        }
    }
}
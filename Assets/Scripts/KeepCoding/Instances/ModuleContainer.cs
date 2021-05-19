using System;
using System.Collections.Generic;
using UnityEngine;
using KModkit.Internal;

namespace KModkit
{
    /// <summary>
    /// Container for both regular and needy modules. Written by Emik.
    /// </summary>
    public sealed class ModuleContainer : IEquatable<ModuleContainer>
    {
        /// <summary>
        /// Encapsulates either a regular or needy module.
        /// </summary>
        /// <exception cref="ConstructorArgumentException"></exception>
        /// <param name="regular">The instance of a normal module.</param>
        /// <param name="needy">The instance of a needy module.</param>
        public ModuleContainer(KMBombModule regular = null, KMNeedyModule needy = null)
        {
            if ((bool)regular == needy)
                throw new ConstructorArgumentException(regular ? "Both KMBombModule and KMNeedyModule are assigned, which will mean that it is unable to return both when calling a function that returns a single MonoBehaviour." : "Both KMBombModule and KMNeedyModule is null, and since this data type is immutable after the constructor, it is unable to return anything.");
            
            _bombModule = regular;
            _needyModule = needy;
        }

        /// <value>
        /// Set to true to only allow this module to be placed on the same face as the timer. Useful when the rules involve the timer in some way (like the Big Button), but should be used sparingly as it limits generation possibilities.
        /// </value>
        /// <exception cref="UnrecognizedTypeException"></exception>
        public bool RequiresTimerVisibility
        {
            get
            {
                if (Module is KMBombModule)
                    return ((KMBombModule) Module).RequiresTimerVisibility;
                if (Module is KMNeedyModule)
                    return ((KMNeedyModule) Module).RequiresTimerVisibility;
                throw _unreachableException;
            }
        }

        /// <value>
        /// The nice display name shown to players. e.g. "The Button"
        /// </value>
        /// <exception cref="UnrecognizedTypeException"></exception>
        public string ModuleDisplayName
        {
            get
            {
                if (Module is KMBombModule)
                    return ((KMBombModule) Module).ModuleDisplayName;
                if (Module is KMNeedyModule)
                    return ((KMNeedyModule) Module).ModuleDisplayName;
                throw _unreachableException;
            }
        }

        /// <value>
        /// The identifier for the module as referenced in missions. e.g. "BigButton" Also known as a "Module ID".
        /// </value>
        /// <exception cref="UnrecognizedTypeException"></exception>
        public string ModuleType
        {
            get
            {
                if (Module is KMBombModule)
                    return ((KMBombModule) Module).ModuleType;
                if (Module is KMNeedyModule)
                    return ((KMNeedyModule) Module).ModuleType;
                throw _unreachableException;
            }
        }

        /// <value>
        /// Call this when the entire module has been solved.
        /// </value>
        /// <exception cref="UnrecognizedTypeException"></exception>
        public Action HandlePass
        {
            get
            {
                if (Module is KMBombModule)
                    return ((KMBombModule) Module).HandlePass;
                if (Module is KMNeedyModule)
                    return ((KMNeedyModule) Module).HandlePass;
                throw _unreachableException;
            }
        }

        /// <value>
        /// Call this on any mistake that you want to cause a bomb strike.
        /// </value>
        /// <exception cref="UnrecognizedTypeException"></exception>
        public Action HandleStrike
        {
            get
            {
                if (Module is KMBombModule)
                    return ((KMBombModule) Module).HandleStrike;
                if (Module is KMNeedyModule)
                    return ((KMNeedyModule) Module).HandleStrike;
                throw _unreachableException;
            }
        }

        /// <value>
        /// Returns the random seed used to generate the rules for this game. Not currently used.
        /// </value>
        /// <exception cref="UnrecognizedTypeException"></exception>
        public Func<int> GetRuleGenerationSeed
        {
            get
            {
                if (Module is KMBombModule)
                    return ((KMBombModule) Module).GetRuleGenerationSeed;
                if (Module is KMNeedyModule)
                    return ((KMNeedyModule) Module).GetRuleGenerationSeed;
                throw _unreachableException;
            }
        }

        /// <value>
        /// Returns <see cref="KMBombModule"/>, or if null, throws a <see cref="NullReferenceException"/>.
        /// </value>
        /// <exception cref="NullReferenceException"></exception>
        public KMBombModule Regular
        {
            get
            {
                return _bombModule.NullCheck("KMBombModule is null, yet you are trying to access it.");
            }
        }

        /// <value>
        /// Returns <see cref="KMNeedyModule"/>, or if null, throws a <see cref="NullReferenceException"/>.
        /// </value>
        /// <exception cref="NullReferenceException"></exception>
        public KMNeedyModule Needy
        {
            get
            {
                return _needyModule.NullCheck("KMNeedyModule is null, yet you are trying to access it.");
            }
        }

        /// <value>
        /// Returns <see cref="KMBombModule"/>, or if null, <see cref="KMNeedyModule"/>.
        /// </value>
        public MonoBehaviour Module
        {
            get
            {
                return _bombModule ?? (MonoBehaviour)_needyModule;
            }
        }

        /// <summary>
        /// Creates a new instance of <see cref="ModuleContainer"/> where <see cref="Regular"/> is defined.
        /// </summary>
        /// <param name="regular">The regular module to create a new <see cref="ModuleContainer"/> of.</param>
        /// <returns>A <see cref="ModuleContainer"/> with parameter <paramref name="regular"/>.</returns>
        public static implicit operator ModuleContainer(KMBombModule regular)
        {
            return new ModuleContainer(regular: regular);
        }

        /// <summary>
        /// Creates a new instance of <see cref="ModuleContainer"/> where <see cref="Needy"/> is defined.
        /// </summary>
        /// <param name="needy">The needy module to create a new <see cref="ModuleContainer"/> of.</param>
        /// <returns>A <see cref="ModuleContainer"/> with parameter <paramref name="needy"/>.</returns>
        public static implicit operator ModuleContainer(KMNeedyModule needy)
        {
            return new ModuleContainer(needy: needy);
        }

        /// <summary>
        /// Returns the instance of <see cref="KMBombModule"/> from <see cref="Regular"/>.
        /// </summary>
        /// <param name="container">The <see cref="ModuleContainer"/> to get the <see cref="KMBombModule"/> from.</param>
        /// <returns>A <see cref="KMBombModule"/> from <see cref="Regular"/>.</returns>
        public static explicit operator KMBombModule(ModuleContainer container)
        {
            return container.Regular;
        }

        /// <summary>
        /// Returns the instance of <see cref="KMNeedyModule"/> from <see cref="Needy"/>.
        /// </summary>
        /// <param name="container">The <see cref="ModuleContainer"/> to get the <see cref="KMNeedyModule"/> from.</param>
        /// <returns>A <see cref="KMBombModule"/> from <see cref="Needy"/>.</returns>
        public static explicit operator KMNeedyModule(ModuleContainer container)
        {
            return container.Needy;
        }

        /// <summary>
        /// Sets the action of OnActivate.
        /// </summary>
        /// <param name="action">The delegate to set.</param>
        public void OnActivate(Action action)
        {
            if (Module is KMBombModule)
                action.Set(ref ((KMBombModule) Module).OnActivate);
            if(Module is KMNeedyModule)
                action.Set(ref ((KMNeedyModule) Module).OnActivate);
            throw _unreachableException;
        }

        /// <summary>
        /// Determines if both <see cref="ModuleContainer"/> variables are equal.
        /// </summary>
        /// <param name="obj">The comparison.</param>
        /// <returns>True if both contain the same instance of <see cref="KMBombModule"/>, <c>null</c>, <see cref="KMNeedyModule"/></returns>
        public override bool Equals(object obj)
        {
            return Equals(obj as ModuleContainer);
        }

        /// <summary>
        /// Determines if both <see cref="ModuleContainer"/> variables are equal.
        /// </summary>
        /// <param name="other">The comparison.</param>
        /// <returns>True if both contain the same instance of <see cref="KMBombModule"/>, <c>null</c>, <see cref="KMNeedyModule"/></returns>
        public bool Equals(ModuleContainer other)
        {
            return Module == other.Module;
        }

        /// <summary>
        /// Gets the current hash code.
        /// </summary>
        /// <returns>The <see cref="Module"/>'s hash code.</returns>
        public override int GetHashCode()
        {
            return 1212890949 + EqualityComparer<MonoBehaviour>.Default.GetHashCode(Module);
        }

        private readonly KMBombModule _bombModule;

        private readonly KMNeedyModule _needyModule;

        private static readonly UnrecognizedTypeException _unreachableException = new UnrecognizedTypeException("Module is neither a KMBombModule or a KMNeedyModule. This is a bug caused by the library, please file a bug report alongside the source code.");
    }
}
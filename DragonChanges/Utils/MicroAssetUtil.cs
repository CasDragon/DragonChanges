﻿using Kingmaker.Blueprints.JsonSystem.Converters;
using Kingmaker.Blueprints;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using static DragonChanges.Utils.TTTHelpers;
using UnityEngine;

namespace DragonChanges.Utils
{
    internal class MicroAssetUtil
    {
        public static Sprite GetAssemblyResourceSprite(string name) =>
            GetSpriteAssemblyResource(Assembly.GetExecutingAssembly(), $"{nameof(DragonChanges)}.Icons.{name}")!;
        public static class Direct
        {
            /// <summary>
            /// Gets a blueprint sprite
            /// </summary>
            public static Sprite GetSprite(string assetId, long fileId) =>
                (Sprite)UnityObjectConverter.AssetList.Get(assetId, fileId);
        }

        /// <summary>Generates a texture from a bitmap (eg. PNG) assembly resource</summary>
        /// <param name="ass">Assembly</param>
        /// <param name="name">Asset file path (in project directory structure)</param>
        /// <param name="format">Texture format</param>
        /// <param name="mipChain">Generate mip chain (see <see cref="Texture2D(int, int, TextureFormat, bool)"/> constructor)</param>
        /// <returns>New texture object</returns>
        public static Texture2D? GetTextureAssemblyResource(Assembly ass, string name, TextureFormat format = TextureFormat.RGBA32, bool mipChain = false)
        {
            name = name.Replace('\\', '.');

            if (!ass.GetManifestResourceNames().Contains(name)) return null;

            using var s = ass.GetManifestResourceStream(name);
            using var bs = new BinaryReader(s);

            var imageData = bs.ReadBytes((int)s.Length);

            var t = new Texture2D(2, 2, format, mipChain);
            t.LoadImage(imageData);
            t.Apply();

            return t;
        }

        /// <summary>
        /// Creates a sprite from a texture
        /// </summary>
        public static Sprite CreateSprite(Texture2D texture, Rect? rect = null, Vector2? pivot = null)
        {
            pivot ??= new(0.5f, 0.5f);
            rect ??= new(0, 0, texture.width, texture.height);

            return Sprite.Create(texture, rect.Value, pivot.Value);
        }

        /// <summary>
        /// Equivalent to <see cref="GetSpriteAssemblyResource(Assembly, string, Rect?, Vector2?)"/> and <see cref="CreateSprite"/>
        /// </summary>
        /// <param name="ass"></param>
        /// <param name="name"></param>
        /// <param name="rect"></param>
        /// <param name="pivot"></param>
        /// <returns></returns>
        public static Sprite? GetSpriteAssemblyResource(Assembly ass, string name, Rect? rect = null, Vector2? pivot = null)
        {
            var t = GetTextureAssemblyResource(ass, name);

            if (t is null) return null;

            return CreateSprite(t, rect, pivot);
        }

        /// <summary>
        /// Clones a blueprint
        /// </summary>
        /// <typeparam name="TBlueprint">Blueprint type</typeparam>
        /// <param name="blueprint">Blueprint to clone</param>
        /// <param name="guid">New blueprint's guid</param>
        /// <param name="name">New blueprint's name</param>
        /// <param name="addToLibrary">Add to library immediately</param>
        /// <returns>Blueprint clone</returns>
        public static TBlueprint CloneBlueprint<TBlueprint>(TBlueprint blueprint, BlueprintGuid guid, string? name = null, bool addToLibrary = true)
            where TBlueprint : SimpleBlueprint
        {
            blueprint = (ObjectDeepCopier.Clone(blueprint) as TBlueprint)!;

            if (blueprint is BlueprintScriptableObject bso)
            {
                foreach (var c in bso.Components)
                {
                    c.OwnerBlueprint = bso;
                }
            }

            blueprint.AssetGuid = guid;

            if (name is not null) blueprint.name = name;

            if (addToLibrary) ResourcesLibrary.BlueprintsCache.AddCachedBlueprint(blueprint.AssetGuid, blueprint);

            return blueprint;
        }

        /// <exclude />
        static readonly Dictionary<string, AssetBundle> loadedBundles = new();

        /// <exclude />
        static AssetBundle LoadBundleFromResource(string name)
        {
            if (!loadedBundles.ContainsKey(name))
            {
                using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(name);

                if (stream is null)
                {
                    var sb = new StringBuilder();
                    sb.AppendLine($"No resource with name {name}");
                    sb.AppendLine($"Assembly resource names:");

                    foreach (var n in Assembly.GetExecutingAssembly().GetManifestResourceNames())
                    {
                        sb.AppendLine($"  {n}");
                    }

                    throw new ArgumentException(sb.ToString());
                }

                loadedBundles[name] = AssetBundle.LoadFromStream(stream);
            }

            return loadedBundles[name];
        }
    }
}

﻿using System;
using System.Drawing;
using Captura;

namespace Screna
{
    /// <summary>
    /// Applies Overlays on an <see cref="IImageProvider"/>.
    /// </summary>
    public class OverlayedImageProvider : IImageProvider
    {
        IOverlay[] _overlays;
        IImageProvider _imageProvider;
        readonly Func<Point, Point> _transform;
        
        /// <summary>
        /// Creates a new instance of <see cref="OverlayedImageProvider"/>.
        /// </summary>
        /// <param name="ImageProvider">The <see cref="IImageProvider"/> to apply the Overlays on.</param>
        /// <param name="Overlays">Array of <see cref="IOverlay"/>(s) to apply.</param>
        /// <param name="Transform">Point Transform Function.</param>
        public OverlayedImageProvider(IImageProvider ImageProvider, Func<Point, Point> Transform, params IOverlay[] Overlays)
        {
            _imageProvider = ImageProvider ?? throw new ArgumentNullException(nameof(ImageProvider));
            _overlays = Overlays ?? throw new ArgumentNullException(nameof(Overlays));
            _transform = Transform ?? throw new ArgumentNullException(nameof(Transform));

            Width = ImageProvider.Width;
            Height = ImageProvider.Height;
        }

        /// <inheritdoc />
        public IEditableFrame Capture()
        {
            var bmp = _imageProvider.Capture();
            
            // Overlays should have already been drawn on previous frame
            if (bmp is RepeatFrame)
            {
                return bmp;
            }
            
            foreach (var overlay in _overlays)
                overlay?.Draw(bmp, _transform);
            
            return bmp;
        }
        
        /// <inheritdoc />
        public int Height { get; }

        /// <inheritdoc />
        public int Width { get; }

        /// <inheritdoc />
        public void Dispose()
        {
            _imageProvider.Dispose();

            foreach (var overlay in _overlays)
                overlay?.Dispose();

            _imageProvider = null;
            _overlays = null;
        }
    }
}

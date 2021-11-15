﻿using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoogleMapsComponents.Maps
{
    /// <summary>
    /// A polyline is a linear overlay of connected line segments on the map.
    /// </summary>
    public class Polyline : JsObjectRef//ListableEntityBase<PolylineOptions>
    {
        /// <summary>
        /// Create a polyline using the passed PolylineOptions, which specify both the path of the polyline and the stroke style to use when drawing the polyline.
        /// </summary>
        public async static Task<Polyline> CreateAsync(IJSRuntime jsRuntime, PolylineOptions opts = null)
        {
            //var jsObjectRef = await JsObjectRef.CreateAsync(jsRuntime, "google.maps.Polyline", opts);

            //var obj = new Polyline(jsObjectRef, opts);

            //return obj;

            throw new NotImplementedException();
        }

        /// <summary>
        /// Constructor for use in ListableEntityListBase. Must be the first constructor!
        /// </summary>
        internal Polyline(JsObjectRef jsObjectRef)
            :base(jsObjectRef)
        {
        }

        /// <summary>
        /// Create a polyline using the passed PolylineOptions, which specify both the path of the polyline and the stroke style to use when drawing the polyline.
        /// </summary>
        private Polyline(JsObjectRef jsObjectRef, PolylineOptions opts)
            :this(jsObjectRef)
        {
        }

        /// <summary>
        /// Returns whether this shape can be dragged by the user.
        /// </summary>
        /// <returns></returns>
        public ValueTask<bool> GetDraggable()
        {
            return InvokeAsync<bool>(
                "getDraggable");
        }

        /// <summary>
        /// Returns whether this shape can be edited by the user.
        /// </summary>
        /// <returns></returns>
        public ValueTask<bool> GetEditable()
        {
            return InvokeAsync<bool>(
                "getEditable");
        }

        /// <summary>
        /// Retrieves the path.
        /// </summary>
        /// <returns></returns>
        public ValueTask<IEnumerable<LatLngLiteral>> GetPath()
        {
            return InvokeAsync<IEnumerable<LatLngLiteral>>("getPath");
        }

        /// <summary>
        /// Returns whether this poly is visible on the map.
        /// </summary>
        /// <returns></returns>
        public ValueTask<bool> GetVisible()
        {
            return InvokeAsync<bool>(
                "getVisible");
        }

        /// <summary>
        /// If set to true, the user can drag this shape over the map. 
        /// The geodesic property defines the mode of dragging.
        /// </summary>
        /// <param name="draggable"></param>
        /// <returns></returns>
        public ValueTask SetDraggable(bool draggable)
        {
            return InvokeVoidAsync(
                "setDraggable",
                draggable);
        }

        /// <summary>
        /// If set to true, the user can edit this shape by dragging the control points shown at the vertices and on each segment.
        /// </summary>
        /// <param name="editable"></param>
        /// <returns></returns>
        public ValueTask SetEditable(bool editable)
        {
            return InvokeVoidAsync(
                "setEditable",
                editable);
        }

        public ValueTask SetOptions(PolylineOptions options)
        {
            return InvokeVoidAsync(
                "setOptions",
                options);
        }

        /// <summary>
        /// Sets the path.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public ValueTask SetPath(IEnumerable<LatLngLiteral> path)
        {
            return InvokeVoidAsync(
                "setPath",
                path);
        }

        /// <summary>
        /// Hides this poly if set to false.
        /// </summary>
        /// <param name="visible"></param>
        /// <returns></returns>
        public ValueTask SetVisible(bool visible)
        {
            return InvokeVoidAsync(
                "setVisible",
                visible);
        }

    }
}

﻿using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoogleMapsComponents.Maps
{
    /// <summary>
    /// A class able to manage a lot of Marker objects and get / set their properties at the same time, eventually with different values
    /// 
    /// Main concept is that each Marker to can be distinguished by other ones need to have a "unique key" with a "external world mean", so not necessary it's GUID
    /// In real cases Markers are be linked to places, activities, transit stops and so on -> So, what better way to choose as Marker "unique key" the "id" of the object each marker is related to?
    /// A string key has been selected as type due to its implicit versatility.
    /// 
    /// To create Markers, simply call MarkerList.CreateAsync with a Dictionary of desired Marker keys and MarkerOptions values
    /// After that, a new instance of MarkerList class will be returned with its Markers dictionary member populated with the corresponding results
    /// 
    /// At run-time is possible to: 
    /// 1) add Marker to the same MarketList class using AddMultipleAsync method (only keys not matching with existent Marker keys will be created)
    ///    Markers dictionary will contains "union distinct" of existent Marker's keys and new keys
    /// 2) remove Marker from the MarketList class (only Marker having keys matching with existent keys will be removed)
    ///    Markers dictionary will contains "original - required and found" Marker's keys (eventually any is all Marker are removed)
    /// 
    /// Each definer getter properties can be used as follow: 
    /// a) without parameter -> all eventually defined markers related property will be returned (if any)
    /// b) with a List<string> of keys -> all eventually mathing keys with Markers Dictionary keys produces related merkers property extracion (if any defined)
    /// 
    /// Each setter properties can be used as follow:
    /// With a Dictionary<string, {property type}> indicating for each Marker (related to that key) the corresponding related property value
    /// </summary>
    public class InfoWindow : JsObjectRef
    {
        public readonly Dictionary<string, List<MapEventListener>> EventListeners = new();

        /// <summary>
        /// Creates an info window with the given options. 
        /// An InfoWindow can be placed on a map at a particular position or above a marker, depending on what is specified in the options. 
        /// Unless auto-pan is disabled, an InfoWindow will pan the map to make itself visible when it is opened. 
        /// After constructing an InfoWindow, you must call open to display it on the map. 
        /// The user can click the close button on the InfoWindow to remove it from the map, or the developer can call close() for the same effect.
        /// </summary>
        /// <param name="opts"></param>
        public async static Task<InfoWindow> CreateAsync(IJSRuntime jsRuntime, InfoWindowOptions opts = null)
        {
            //var jsObjectRef = await JsObjectRef.CreateAsync(jsRuntime, "google.maps.InfoWindow", opts);

            //var obj = new InfoWindow(jsObjectRef, opts);

            //return obj;

            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates an info window with the given options. 
        /// An InfoWindow can be placed on a map at a particular position or above a marker, depending on what is specified in the options. 
        /// Unless auto-pan is disabled, an InfoWindow will pan the map to make itself visible when it is opened. 
        /// After constructing an InfoWindow, you must call open to display it on the map. 
        /// The user can click the close button on the InfoWindow to remove it from the map, or the developer can call close() for the same effect.
        /// </summary>
        /// <param name="opts"></param>
        private InfoWindow(IJSObjectReference jsObjectRef)
            : base(jsObjectRef)
        {
        }

        public async ValueTask DisposeAsync()
        {
            EventListeners.Clear();

            await base.DisposeAsync();
        }

        /// <summary>
        /// Closes this InfoWindow by removing it from the DOM structure.
        /// </summary>
        public ValueTask Close()
        {
            return this.InvokeVoidAsync("close");
        }

        public ValueTask<string> GetContent()
        {
            return this.InvokeAsync<string>("getContent");
        }

        public ValueTask<LatLngLiteral> GetPosition()
        {
            return this.InvokeAsync<LatLngLiteral>("getPosition");
        }

        public ValueTask<int> GetZIndex()
        {
            return this.InvokeAsync<int>("getZIndex");
        }


        /// <summary>
        /// Opens this InfoWindow on the given map.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="anchor"></param>
        public ValueTask Open(Map map, object anchor = null)
        {
            return this.InvokeVoidAsync("open", map, anchor);
        }

        public ValueTask SetContent(string content)
        {
            return this.InvokeVoidAsync("setContent", content);
        }

        public ValueTask SetPosition(LatLngLiteral position)
        {
            return this.InvokeVoidAsync(
                "setPosition",
                position);
        }

        public ValueTask SetZIndex(int zIndex)
        {
            return this.InvokeVoidAsync(
                "setZIndex",
                zIndex);
        }

        public async ValueTask<MapEventListener> AddListener(string eventName, Action handler)
        {
            var listenerRef = await this.InvokeAsync<IJSObjectReference>("addListener", eventName, handler);
            var eventListener = new MapEventListener(listenerRef);

            if (!EventListeners.ContainsKey(eventName))
            {
                EventListeners.Add(eventName, new List<MapEventListener>());
            }
            EventListeners[eventName].Add(eventListener);

            return eventListener;
        }

        public async ValueTask<MapEventListener> AddListener<T>(string eventName, Action<T> handler)
        {
            var listenerRef = await this.InvokeAsync<IJSObjectReference>("addListener", eventName, handler);
            var eventListener = new MapEventListener(listenerRef);

            if (!EventListeners.ContainsKey(eventName))
            {
                EventListeners.Add(eventName, new List<MapEventListener>());
            }
            EventListeners[eventName].Add(eventListener);

            return eventListener;
        }

        public async ValueTask ClearListeners(string eventName)
        {
            if (EventListeners.ContainsKey(eventName))
            {
                await this.InvokeVoidAsync("clearListeners", eventName);

                //IMHO is better preserving the knowledge that Marker had some EventListeners attached to "eventName" in the past
                //so, instead to clear the list and remove the key from dictionary, I prefer to leave the key with an empty list
                EventListeners[eventName].Clear();
            }
        }
    }
}

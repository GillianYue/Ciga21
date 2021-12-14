// This file is provided under The MIT License as part of Steamworks.NET.
// Copyright (c) 2013-2019 Riley Labrecque
// Please see the included LICENSE.txt for additional information.

// This file is automatically generated.
// Changes to this file will be reverted when you update Steamworks.NET

#if !(UNITY_STANDALONE_WIN || UNITY_STANDALONE_LINUX || UNITY_STANDALONE_OSX || STEAMWORKS_WIN || STEAMWORKS_LIN_OSX)
#define DISABLESTEAMWORKS
#endif

#if !DISABLESTEAMWORKS

using System.Runtime.InteropServices;
using IntPtr = System.IntPtr;

namespace Steamworks {
	[System.Serializable]
	public struct HServerListRequest : System.IEquatable<HServerListRequest> {
		public static readonly HServerListRequest Invalid = new HServerListRequest(System.IntPtr.Zero);
		public System.IntPtr m_HServerListRequest;

		public HServerListRequest(System.IntPtr value) {
			m_HServerListRequest = value;
		}

		public override string ToString() {
			return m_HServerListRequest.ToString();
		}

		public override bool Equals(object other) {
			return other is HServerListRequest && this == (HServerListRequest)other;
		}

		public override int GetHashCode() {
			return m_HServerListRequest.GetHashCode();
		}

		public static bool operator ==(HServerListRequest x, HServerListRequest y) {
			return x.m_HServerListRequest == y.m_HServerListRequest;
		}

		public static bool operator !=(HServerListRequest x, HServerListRequest y) {
			return !(x == y);
		}

		public static explicit operator HServerListRequest(System.IntPtr value) {
			return new HServerListRequest(value);
		}

		public static explicit operator System.IntPtr(HServerListRequest that) {
			return that.m_HServerListRequest;
		}

		public bool Equals(HServerListRequest other) {
			return m_HServerListRequest == other.m_HServerListRequest;
		}
	}
}

#endif // !DISABLESTEAMWORKS

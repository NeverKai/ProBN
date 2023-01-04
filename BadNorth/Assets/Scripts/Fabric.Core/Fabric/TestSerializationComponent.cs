using System;
using UnityEngine;
using System.Collections.Generic;
using Object = UnityEngine.Object;

namespace Fabric
{
	public class TestSerializationComponent : MonoBehaviour
	{
		[Serializable]
		public class SubClass
		{
			[SerializeField]
			public int _someNumber;
		}

		[SerializeField]
		public int _someNumber;
		[SerializeField]
		public int[] _someNumbers;
		[SerializeField]
		public SubClass _subClass;
		[SerializeField]
		public SubClass[] _subClasses;
		[SerializeField]
		public Object _object;
		[SerializeField]
		public Object[] _objects;
		[SerializeField]
		public List<Object> _objectList;
		[SerializeField]
		public TestSerializationComponent _componentReference;
		[SerializeField]
		public AnimationCurve _animCurve;
		[SerializeField]
		public Vector3 _vector;
		[SerializeField]
		public Vector3[] _vectorArray;
		[SerializeField]
		public List<Vector3> _vectorList;
	}
}

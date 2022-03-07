using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

public class MonoPool<T> where T : MonoBehaviour
{
	private readonly Func<T> _createFunction;
	private readonly Action<T> _actionOnGet;
	private readonly Action<T> _actionOnRelease;
	private readonly Action<T> _actionOnDestroy;
	private readonly int _defaultCapacity;
	private readonly bool _autoExpand;
	private Transform _container;

	private List<T> _pool;

	public MonoPool(Func<T> createFunction, Action<T> actionOnGet, Action<T> actionOnRelease, Action<T> actionOnDestroy, 
		int defaultCapacity, bool autoExpand)
	{
		_createFunction = createFunction;
		_actionOnGet = actionOnGet;
		_actionOnRelease = actionOnRelease;
		_actionOnDestroy = actionOnDestroy;
		_defaultCapacity = defaultCapacity;
		_autoExpand = autoExpand;
	}

	~MonoPool()
	{
		Dispose();
	}

	public T GetElement()
	{
		if (HasFreeElement(out T element))
		{
			_actionOnGet.Invoke(element);
			return element;
		}

		if (_autoExpand)
		{
			T createdElement = CreateObject();
			_actionOnGet.Invoke(createdElement);
			return createdElement;
		}

		throw new Exception($"В пуле нет свободных объектов типа {typeof(T)}");
	}

	public void ReleaseElement(T element)
	{
		_actionOnRelease.Invoke(element);
	}

	public void Dispose()
	{
		foreach (T poolObject in _pool)
		{
			_actionOnDestroy.Invoke(poolObject);
		}
	}

	public void CreatePool(Transform container)
	{
		_container = container;
		_pool = new List<T>();
		for (int i = 0; i < _defaultCapacity; i++)
		{
			CreateObject();
		}
	}

	private T CreateObject(bool isActiveByDefault = false)
	{
		T createdObject = _createFunction.Invoke();
		createdObject.gameObject.SetActive(isActiveByDefault);
		createdObject.transform.parent = _container;
		_pool.Add(createdObject);
		return createdObject;
	}

	private bool HasFreeElement(out T element)
	{
		foreach (T elem in _pool.Where(elem => !elem.gameObject.activeInHierarchy))
		{
			element = elem;
			elem.gameObject.SetActive(true);
			return true;
		}

		element = null;
		return false;
	}
}

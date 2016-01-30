using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public static class Utils {

	public static void randomizeArray(int[] array){
		for (int i = 0; i < array.Length; i++){
			swap(array, i, UnityEngine.Random.Range(i, array.Length));
		}
	}

		
	public static void  swap(int[] arr , int id1, int id2){
		var temp = arr[id1];
		arr[id1] = arr[id2];
		arr[id2] = temp;
	}

	
	static public T mGetRandomListElement<T>(List<T> aList) {
		if (aList.Count	== 0) {
			throw new Exception("The list can't be empty");	
		}		
		int vIndex = (int)(Mathf.Floor(aList.Count * UnityEngine.Random.value));		
		return aList[vIndex];
	}
	
	
	/**
 * @param	aTime
 * @return time representation in format MM:SS
 */
	static public string mGetTimeString(float aTime) {
		int vMin = (int)(aTime / 60);
		int vSeg = (int)(aTime - 60 * vMin);
		string vSegStr;
		if (vSeg < 10) {
			vSegStr = "0" + vSeg;
		} else {
			vSegStr = "" + vSeg;
		}
		return "" + vMin + ":" + vSegStr;
	}
	
	
	/**
 * @param	aFrequencies an array of frequencies (Numbers)
 * e.g. {0.3, 0.2, 0.5}
 * @return  A random int, that's an index of aFrequencies, randomized according to the
 * frequencies. 
 * @example If aFrequencies == {x} it will always return 0.
 * If aFrequencies == {0.1, 0.9}, it will return 0 with a frequency of 1/10, and
 * 1 with a frequency of 9/10.
 */
	static public int mGetRandomIntFromFrequencies(List<float> aFrequencies) {
		if (aFrequencies == null) {
			throw new ArgumentException("The frequency array can't be null.");
		}
		
		aFrequencies = mNormalizeFrequenciesList(aFrequencies);		
		
		/*
	if (aFrequencies.Exists(x => x.ToString() == "NaN")) {			
		mPrintList(aFrequencies);
		throw new Exception("NaN at index " + aFrequencies.IndexOf(aFrequencies.Find(x => x.ToString() == "NaN")));;
	}
	*/
		
		float vRandom = UnityEngine.Random.value;
		float vFrequencyAccumulator = 0;
		
		for (int i = 0; i < aFrequencies.Count; i++) {
			vFrequencyAccumulator += aFrequencies[i];
			
			if (aFrequencies[i].ToString() == "NaN") {
				throw new Exception("NaN");
			}
			
			if (vRandom < vFrequencyAccumulator) {
				return i;
			}
		}
		mPrintList(aFrequencies);
		throw new Exception("This shouldn't happen! The function is badly implemented!");
	}
	
	
	
	public static List<float> mNormalizeFrequenciesList(List<float> aFrequencies) {
		float vSumFrequencies = mGetListSum(aFrequencies);
		
		if (vSumFrequencies == 0) {
			mPrintList(aFrequencies);
			throw new Exception("Sum Frequencies = 0");	
		}
		List<float> vNormalizedFrequencies = new List<float>();
		for (int i=0;i<aFrequencies.Count;i++) {
			vNormalizedFrequencies.Add(aFrequencies[i]/vSumFrequencies);
			
		}
		return vNormalizedFrequencies;
	}
	
	
	/**
 * @param aArray an array of any type
 * @param	aFrequencies an array of frequencies (floats), whose sum is 1.0, e.g. {0.3f, 0.2f, 0.5f}
 * @return the element of aArray with index equal to mGetRandomIntFromFrequencies(aFrequencies)
 * @see mGetRandomIntFromFrequencies()
 * @throws ArgumentError if the lengths of aArray and aFrequencies are not equal.
 */
	static public T mGetRandomListElementFromFrequencies<T>(List<T> aList,
	                                                        List<float> aFrequencies) {
		if (mGetListSum(aFrequencies) == 0) {
			mPrintList(aList);
			throw new ArgumentException("The sum of the frequencies can't be 0");	
		}
		
		if (aList.Count != aFrequencies.Count) {
			throw new ArgumentException("The lengths of aArray and aFrequencies must be equal.");
		}
		int vIndex = mGetRandomIntFromFrequencies(aFrequencies);
		return aList[vIndex];
	}
	
	
	static public float mDistance(float aNum1, float aNum2) {
		return Math.Abs(aNum1 - aNum2);
	}
	
	
	/**
 * Returns the sum of the elements of a list.	 
 */
	static public float mGetListSum(List<float> aList) {
		float vSum = 0;
		for (int i = 0; i < aList.Count; i++) {
			vSum += aList[i];
		}
		return vSum;
	}
	
	
	static public List<T> mGetRange<T>(this List<T> aList, int aFirstIndex, int aLastIndex) {
		List<T> vList = new List<T>();
		for (int i=aFirstIndex;i<= Math.Min(aLastIndex,aList.Count-1);i++) {
			vList.Add(aList[i]);
		}
		return vList;
		//return aList.GetRange(aFirstIndex, aLastIndex - aFirstIndex + 1);
	}
	
	
	static public void mRemoveAllFromList<T>(this List<T> aGoodList, List<T> aBadList) {
		aGoodList.RemoveAll(x => aBadList.Contains(x));
	}
	
	
	static public T mGetRandomListElementDifferentFrom<T>(List<T> aSourceList, List<T> aExcludedList) {
		return mGetRandomListElement(mSubstractList(aSourceList, aExcludedList));
	}
	
	
	static public T mGetRandomListElementDifferentFrom<T>(List<T> aSourceList, T aExcludedElement) {
		List<T> vList = new List<T>();
		for (int i=0;i< aSourceList.Count;i++) {
			if (!aSourceList[i].Equals(aExcludedElement)) {
				vList.Add(aSourceList[i]);
			}
		}
		return mGetRandomListElement(vList);
		
	}	
	
	
	static public T mGetRandomListElementFromFrequenciesDifferentFrom<T>(List<T> aSourceList, 
	                                                                     List<float> aFrequencies, List<T> aExcludedList) {
		List<T> vIncludedElements = mSubstractList(aSourceList, aExcludedList);
		//I want a new frequencies list (vIncludedFrequencies) that corresponds to vIncludedElements
		List<float> vIncludedFrequencies = new List<float>();
		foreach (T vIncludedElement in vIncludedElements) {
			vIncludedFrequencies.Add(mGetFrequency(vIncludedElement, aSourceList, aFrequencies));
		}
		if (mGetListSum(vIncludedFrequencies) == 0) {
			throw new ArgumentException("");
		}		
		return mGetRandomListElementFromFrequencies(vIncludedElements, vIncludedFrequencies);
	}
	
	
	//Get the frequency of a element from the elements list and the frequencies list
	static private float mGetFrequency<T>(T aElement, List<T> aElements, List<float> aFrequencies) {
		int vIndexOfElement = aElements.IndexOf(aElement);
		float vFrequency;
		try {
			vFrequency = aFrequencies[vIndexOfElement];
		}
		catch (Exception e) {
			Debug.Log("aElement: " + aElement);
			Debug.Log("aElements:");
			mPrintList(aElements);
			Debug.Log("aFrequencies:");
			mPrintList(aFrequencies);
			throw e;
		}
		return vFrequency;
	}	
	
	
	static public List<T> mGetCommonElements<T>(List<T> aList1, List<T> aList2) {
		List<T> vList = new List<T>();
		for (int i=0;i< aList1.Count;i++) {
			if (aList2.Contains(aList1[i])) {
				vList.Add(aList1[i]);
			}
		}
		
		return vList;
	}
	
	
	static public int mGetNumCommonElements<T>(List<T> aList1, List<T> aList2) {
		return mGetCommonElements(aList1, aList2).Count;
	}	
	
	
	//Get all the elements that are in aList1 but not in aList2
	static public List<T> mSubstractList<T>(List<T> aList1, List<T> aList2) {
		List<T> vList = new List<T>();
		for (int i=0;i< aList1.Count;i++) {
			if (!aList2.Contains(aList1[i])) {
				vList.Add(aList1[i]);
			}
		}
		return vList;
	}
	
	
	static public int mGetNumNullsInList<T>(List<T> aList) {
		int vCount =0;
		for (int i=0;i< aList.Count;i++) {
			if (aList[i]==null) {
				vCount++;
			}
		}
		return vCount;
	}
	
	
	static public float mDegreesToRadians(float aAngleDegrees) {
		return aAngleDegrees / 360 * (2 * Mathf.PI);
	}
	
	
	static public float mRadiansToDegrees(float aAngleRadians) {
		return aAngleRadians * 360 / (2 * Mathf.PI);
	}
	
	
	public static TKey mFindKeyByValue<TKey, TValue>(this IDictionary<TKey, TValue> aDictionary, TValue aValue) {
		if (aDictionary == null) {
			throw new ArgumentNullException("dictionary");
		}
		foreach (KeyValuePair<TKey, TValue> vPair in aDictionary) {
			if (aValue.Equals(vPair.Value)) { 
				return vPair.Key;
			}            
		}
		throw new Exception("The value is not found in the dictionary");
	}
	
	
	public static List<TValue> mGetValuesList<TKey, TValue>(this Dictionary<TKey, TValue> aDictionary) {
		List<TValue> vList = new List<TValue>();
		foreach (TKey vKey in aDictionary.Keys) {
			vList.Add(aDictionary[vKey]);	
		}
		return vList;
	}
	
	
	static public float mGetRandomNumberBetween(float aNumber1, float aNumber2) {
		return aNumber1 + UnityEngine.Random.value * (aNumber2 - aNumber1);
	}
	
	
	static public int mGetRandomIntBetween(int aInt1, int aInt2) {
		return aInt1 + (int)Mathf.Floor(UnityEngine.Random.value * (aInt2 - aInt1 + 1));
	}
	
	
	//Returns a color in RGB from 0 to 1 from RGB values from 0 to 256
	static public Color mGetColor(float aRed, float aGreen, float aBlue) {
		return new Color(aRed/256f, aGreen/256f, aBlue/256f);	
	}
	
	
	//Returns true iff aObject is equal to aObject2, aObject3, aObject4 or aObject5
	static public bool mIsAnyOf(this object aObject, object aObject2, object aObject3, object aObject4, object aObject5) {
		return aObject.Equals(aObject2)
			|| aObject.Equals(aObject3)
				|| aObject.Equals(aObject4)
				|| aObject.Equals(aObject5);
	}	
	
	//Returns true iff aObject is equal to aObject2, aObject3 or aObject4
	static public bool mIsAnyOf(this object aObject, object aObject2, object aObject3, object aObject4) {
		return aObject.Equals(aObject2) 
			|| aObject.Equals(aObject3)
				|| aObject.Equals(aObject4);
	}
	
	
	static public bool mIsAnyOf(this object aObject, object aObject2, object aObject3) {
		return aObject.Equals(aObject2) 
			|| aObject.Equals(aObject3);
	}
	
	
	static public void mPrintList<T> (List<T> aList) {
		string vStr = "[";
		for (int i=0; i<aList.Count; i++) {
			vStr += aList[i].ToString();
			if (i < aList.Count	- 1) {
				vStr += ",";	
			}
		}		
		vStr += "]";
		Debug.Log(vStr);
	}
	
	
	static public string mGetStringFromFile(string aFilePath) {
		var vFile = Resources.Load(aFilePath);
		if (vFile == null) {
			return null;	
		}
		else {
			return (vFile as TextAsset).text;
		}
	}
	
	
	/**		 
 * @param	aVector Vector to rotate
 * @param	aAngle Rotation angle in degrees
 * @return
 */
	static public Vector2 mRotateVector(Vector2 aVector, float aAngle) {
		if (aAngle == 0) {
			return aVector;
		} else {
			float vAngle = mDegreesToRadians(aAngle);
			float vX = aVector.x * Mathf.Cos(vAngle) + aVector.y * Mathf.Sin(vAngle);
			float vY = aVector.y * Mathf.Cos(vAngle) - aVector.x * Mathf.Sin(vAngle);
			return new Vector2(vX, vY);
		}
	}
	
	
	static public Vector2 mGetOppositeVector(Vector2 aVector) {
		return new Vector2(0, 0) - aVector;
	}
	
	
	static public List<T> ArrayToList<T>(T[] array)
	{
		List<T> list = new List<T>();
		list.AddRange(array);
		return list;
	}
	
	
	static public bool IsBetweenNumbers(float number, float min, float max)
	{
		return number >= min && number <= max;
	}
	
	
	static public List<T> GetDictionaryKeys<T, U>(Dictionary<T, U> dictionary)
	{
		List<T> keys = new List<T>();
		foreach (T key in dictionary.Keys) {
			keys.Add(key);
		}
		return keys;
	}
	
	
	static public List<U> GetDictionaryValues<T, U>(Dictionary<T, U> dictionary)
	{
		List<U> values = new List<U>();
		foreach (U value in dictionary.Values) {
			values.Add(value);
		}
		return values;
	}
	
	static public bool IsInScreen(Vector2 pos, float xTolerance = 0) 
	{
		return Utils.IsBetweenNumbers(Camera.main.WorldToViewportPoint(pos).x, 0, 1 + xTolerance) &&
			Utils.IsBetweenNumbers(Camera.main.WorldToViewportPoint(pos).y, 0, 1);
	}
	
	
	static public Vector2 Vector3To2(Vector3 vector3)
	{
		return new Vector2(vector3.x, vector3.y);
	}
}

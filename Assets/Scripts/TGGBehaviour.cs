using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TGGBehaviour : MonoBehaviour

{

	public delegate void OnPauseEvent();
	public static event OnPauseEvent OnPause;
	public delegate void OnResumeEvent();
	public static event OnResumeEvent OnResume;

	public const float PAUSED_TIME_SCALE = 0.00001f;

	bool isOn = false;

    public GameObject GetChild(string name)
    {
        return gameObject.transform.FindChild(name).gameObject;
    }


    public bool HasChild(string name)
    {
        return gameObject.transform.FindChild(name) != null;
    }


    public GameObject GetBrother(string name)
    {
        return gameObject.transform.parent.FindChild(name).gameObject;
    }


    public bool HasBrother(string name)
    {
        return gameObject.transform.parent != null && gameObject.transform.parent.FindChild(name) != null;
    }


    public Vector2 ViewportToWorldPoint(Camera camera, float x, float y)
    {
        return camera.ViewportToWorldPoint(new Vector2(x, y));
    }


	public Vector2 ViewportToWorldPoint(float x, float y)
	{
		return Camera.main.ViewportToWorldPoint (new Vector2 (x, y));
	}


    public float ViewportToWorldX(Camera camera, float viewportX)
    {
        return ViewportToWorldPoint(camera, viewportX, 0).x;
    }


	public float ViewportToWorldX(float viewportX)
	{
		return ViewportToWorldPoint(viewportX, 0).x;
	}


    public float ViewportToWorldY(Camera camera, float viewportY)
    {
        return ViewportToWorldPoint(camera, 0, viewportY).y;
    }


	public float ViewportToWorldY(float viewportY)
	{
		return ViewportToWorldPoint(0, viewportY).y;
	}


    public float ViewportToScreenX(Camera camera, float viewportX)
    {
        return camera.ViewportToScreenPoint(new Vector2(viewportX, 0)).x;
    }


    public float ViewportToScreenY(Camera camera, float viewportY)
    {
        return camera.ViewportToScreenPoint(new Vector2(0, viewportY)).y;
    }

	
	public float ViewportToScreenX(float viewportX)
	{
		return Camera.main.ViewportToScreenPoint(new Vector2(viewportX, 0)).x;
	}


	public float ViewportToScreenY(float viewportY)
	{
		return Camera.main.ViewportToScreenPoint(new Vector2(0, viewportY)).y;
	}


	public float WorldToViewportY(float worldY)
	{
		return Camera.main.WorldToViewportPoint(new Vector2(0, worldY)).y;
	}


	public float WorldToViewportX(float worldX)
	{
		return Camera.main.WorldToViewportPoint(new Vector2(worldX, 0)).x;
	}


    public void Assert(bool condition)
    {
        if (!condition) {
            throw new UnityException("Assert failed");
        }
    }


    public bool IsCurrentChildAnimState(string name)
    {
        return GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName(name);
    }


    virtual public bool IsCurrentAnimState(string name)
    {
        return GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName(name);
    }


    public bool IsInSpriteLoader()
    {
        return transform.parent != null && transform.parent.name == "SpriteLoader";
    }


    public TextMesh GetTextMesh()
    {
        return gameObject.GetComponentInChildren<TextMesh>();
    }


    public SpriteRenderer GetSpriteRenderer()
    {
        return gameObject.GetComponent<SpriteRenderer>();
    }


    public GameObject FindGameObject(string name)
    {
        return transform.Find(name).gameObject;
    }


    virtual public void Hide()
    {
        gameObject.SetActive(false);
		isOn = false;
    }


    virtual public void Show()
    {
        gameObject.SetActive(true);
		isOn = true;
    }


	public bool IsOn()
	{
		return isOn;
	}


    public float GetX(MonoBehaviour behavior)
    {
        return behavior.gameObject.transform.position.x;
    }


    public float GetX(GameObject go)
    {
        return go.transform.position.x;
    }


    public float GetX()
    {
        return transform.position.x;
    }


    public float GetWidth(GameObject go)
    {
        return go.transform.GetComponent<Collider>().bounds.size.x;
    }


	protected virtual void PauseGame()
	{
		Time.timeScale = PAUSED_TIME_SCALE;
        if(OnPause != null){
            OnPause();
        }
	}
	
	
	protected virtual void ResumeGame(float timeScale = 1)
	{
		Time.timeScale = timeScale;
		if(OnResume != null){
			OnResume();
		}
	}

}


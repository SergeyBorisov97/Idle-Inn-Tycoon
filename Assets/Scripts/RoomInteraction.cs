using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum RoomState { Locked, Unlocked }

public class RoomInteraction : MonoBehaviour
{
    [SerializeField]
    private GameObject room;
    [SerializeField]
    private GameObject blockingVolume;

    public UnityEngine.UI.Text unlockingText;

    public RoomState roomState;

    private void Awake()
    {
        room = this.gameObject;
    }

    void Start()
    {
        roomState = RoomState.Locked;
        if(!blockingVolume)
            blockingVolume = room.GetChildByName("BlockingVolume").gameObject;
        if(!unlockingText)
            LoadTextAsset();
        Canvas canvas = FindObjectOfType<Canvas>();
        unlockingText.transform.SetParent(canvas.transform, false);
        unlockingText.transform.position = Utils.WorldToScreen(Camera.main, blockingVolume.transform.position);
        unlockingText.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(roomState == RoomState.Locked)
                Unlock();
        }
    }

    void LoadTextAsset()
	{
        UnityEngine.UI.Text prefab = Resources.Load<UnityEngine.UI.Text>("Models/Prefabs/UI/RoomUnlockText");
        unlockingText = Instantiate(prefab);
    }

	private void OnTriggerExit(Collider other)
	{
        if(other.gameObject.tag == "Player")
        {
            if(roomState == RoomState.Unlocked)
                Lock();
        }
    }

	void Lock()
	{
        roomState = RoomState.Locked;
        blockingVolume.SetActive(true);
        HideText();
	}

    void Unlock()
	{
        roomState = RoomState.Unlocked;
        blockingVolume.SetActive(false);
        ShowText();
	}

    void ShowText()
	{
        unlockingText.enabled = true;
        unlockingText.GetComponent<Animation>().Play();
	}

    void HideText()
	{
        unlockingText.enabled = false;
	}
}

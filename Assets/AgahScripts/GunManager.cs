using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GunManager : MonoBehaviour
{
    [SerializeField]List<Gun> _guns = new(5);
    private Gun currentGun;

    public static event UnityAction OnShoot;
    public static event UnityAction OnReload;
    public static event UnityAction<Gun.GunUpgrade> OnUpgrade;

    private void Start()
    {
        foreach (var gun in _guns)
        {
            gun.gameObject.SetActive(false);
        }
        _guns[0].gameObject.SetActive(true);
        currentGun = _guns[0];
    }
    private void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        #region (SwitchGun)
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchGun(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchGun(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchGun(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SwitchGun(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SwitchGun(4);
        }
        #endregion

        if (Input.GetMouseButtonDown(0))
        {
            OnShoot?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            OnReload?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            OnUpgrade?.Invoke(currentGun.GetNextUpgrade());
        }
    }

    void SwitchGun(int index)
    {
        Debug.Log("Switching to gun " + index);
        currentGun.gameObject.SetActive(false);
        currentGun = _guns[index];
        currentGun.gameObject.SetActive(true);
    }

}

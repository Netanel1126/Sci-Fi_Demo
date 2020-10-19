using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float gravity = 9.81f;
    [SerializeField] private GameObject muzzleFlash;
    [SerializeField] private GameObject hitMarkerPrefab;
    [SerializeField] private int currentAmmo;
    [SerializeField] private GameObject weapon;

    private CharacterController controller;
    private AudioSource weaponAudioSource;
    private const int maxAmmo = 50;
    private bool isRealoding;
    private int coins;
    private bool isFirstReload = true;

    private int CurrentAmmo
    {
        get
        {
            return currentAmmo;
        }
        set
        {
            currentAmmo = value;
            UIManager.SharedInstance.UpdateAmmoText(currentAmmo);
            if(currentAmmo == 0 && isFirstReload)
            {
                UIManager.SharedInstance.ShowIndectorWithKey("R");
            }
        }
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        CurrentAmmo = maxAmmo;
    }

    private void Update()
    {
        CalculateMovment();

        if (weapon.activeSelf)
        {
            if (Input.GetMouseButton(0) && CurrentAmmo > 0 && !isRealoding)
            {
                Fire();
            }
            else
            {
                StopFiring();
            }
        }

        if (Input.GetKey(KeyCode.R) && !isRealoding && CurrentAmmo < 50)
        {
            isFirstReload = false;
            UIManager.SharedInstance.HideIndector();
            StartCoroutine(Reload());
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void CalculateMovment()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical);
        Vector3 velocity = direction * speed;

        velocity.y -= gravity;
        velocity = transform.TransformDirection(velocity);

        controller.Move(velocity * Time.deltaTime);
    }

    private void Fire()
    {
        Ray rayOrigin = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hitInfo;

        muzzleFlash.SetActive(true);
        CurrentAmmo--;

        if (!weaponAudioSource.isPlaying)
        {
            weaponAudioSource.Play();
        }

        if (Physics.Raycast(rayOrigin, out hitInfo, Mathf.Infinity))
        {
            GameObject prefub = Instantiate(hitMarkerPrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
            Destructable destructable = hitInfo.transform.GetComponent<Destructable>();
            if (destructable)
            {
                destructable.DestroyCrate();
            }
            Destroy(prefub, 1f);
        }
    }

    private void StopFiring()
    {
        muzzleFlash.SetActive(false);
        weaponAudioSource.Stop();
    }

    private IEnumerator Reload()
    {
        isRealoding = true;
        yield return new WaitForSeconds(1.5f);
        CurrentAmmo = maxAmmo;
        isRealoding = false;
    }

    public void AddCoin()
    {
        coins++;
        UIManager.SharedInstance.ShouldShowCoin(true);
    }

    public void RemoveCoin()
    {
        coins--;

        if(coins <= 0)
            UIManager.SharedInstance.ShouldShowCoin(false);
    }

    public int GetCoins()
    {
        return coins;
    }

    public void EnableWeapon()
    {
        weapon.SetActive(true);
        weaponAudioSource = GetComponentInChildren<AudioSource>();
    }
}

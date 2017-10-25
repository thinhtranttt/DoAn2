using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerInventory : MonterFollow
{
    public GameObject inventory;
    public GameObject characterSystem;
    public GameObject craftSystem;
    private Inventory craftSystemInventory;
    private CraftSystem cS;
    private Inventory mainInventory;
    private Inventory characterSystemInventory;
    private Tooltip toolTip;

    private InputManager inputManagerDatabase;
    public GameObject HPMANACanvas;

    Text hpText;
    Text manaText;
    Image hpImage;
    Image manaImage;

    Text damageText;
    Text ArmorText;


    public float maxHealth = 1000;
    float maxMana = 100;
    float maxDamage = 0;
    float maxArmor = 0;

    public float currentHealth = 900;
    public float currentMana = 70;
    public float currentDamage = 40;
    public float currentArmor = 20;

    int normalSize = 3;


    public void OnEnable()
    {
        Inventory.ItemEquip += OnBackpack;
        Inventory.UnEquipItem += UnEquipBackpack;

        Inventory.ItemEquip += OnGearItem;
        Inventory.ItemConsumed += OnConsumeItem;
        Inventory.UnEquipItem += OnUnEquipItem;

        Inventory.ItemEquip += EquipWeapon;
        Inventory.UnEquipItem += UnEquipWeapon;
    }

    public void OnDisable()
    {
        Inventory.ItemEquip -= OnBackpack;
        Inventory.UnEquipItem -= UnEquipBackpack;

        Inventory.ItemEquip -= OnGearItem;
        Inventory.ItemConsumed -= OnConsumeItem;
        Inventory.UnEquipItem -= OnUnEquipItem;

        Inventory.UnEquipItem -= UnEquipWeapon;
        Inventory.ItemEquip -= EquipWeapon;
    }

    void EquipWeapon(Item item)
    {
        if (item.itemType == ItemType.Weapon)
        {

            updateItemEquip(item);
        }
        if (item.itemType == ItemType.Chest)
        {

            updateItemEquip(item);
        }
        if (item.itemType == ItemType.Earrings)
        {

            updateItemEquip(item);
        }
        if (item.itemType == ItemType.Hands)
        {

            updateItemEquip(item);
        }
        if (item.itemType == ItemType.Head)
        {

            updateItemEquip(item);
        }
        if (item.itemType == ItemType.Necklace)
        {

            updateItemEquip(item);
        }
        if (item.itemType == ItemType.Ring)
        {

            updateItemEquip(item);
        }
        if (item.itemType == ItemType.Shoe)
        {

            updateItemEquip(item);
        }
        if (item.itemType == ItemType.Trouser)
        {

            updateItemEquip(item);
        }
    }

    void UnEquipWeapon(Item item)
    {
        if (item.itemType == ItemType.Weapon)
        {

            updateItemUnequip(item);
        }
        if (item.itemType == ItemType.Chest)
        {

            updateItemUnequip(item);
        }
        if (item.itemType == ItemType.Earrings)
        {

            updateItemUnequip(item);
        }
        if (item.itemType == ItemType.Hands)
        {

            updateItemUnequip(item);
        }
        if (item.itemType == ItemType.Head)
        {

            updateItemUnequip(item);
        }
        if (item.itemType == ItemType.Necklace)
        {

            updateItemUnequip(item);
        }
        if (item.itemType == ItemType.Ring)
        {

            updateItemUnequip(item);
        }
        if (item.itemType == ItemType.Shoe)
        {

            updateItemUnequip(item);
        }
        if (item.itemType == ItemType.Trouser)
        {

            updateItemUnequip(item);
        }
    }

    void updateItemUnequip(Item item)
    {
        for (int i = 0; i < item.itemAttributes.Count; i++)
        {
            if (item.itemAttributes[i].attributeName == "Health")
            {
                if(currentHealth == maxHealth + item.itemAttributes[i].attributeValue)
                {
                    currentHealth -= item.itemAttributes[i].attributeValue;
                }
            }
            if (item.itemAttributes[i].attributeName == "Mana")
            {
                if(currentMana == maxMana + item.itemAttributes[i].attributeValue)
                {
                    currentMana -= item.itemAttributes[i].attributeValue;
                }
            }
            if (item.itemAttributes[i].attributeName == "Armor")
            {
                if ((currentArmor - item.itemAttributes[i].attributeValue) < 0)
                    currentArmor = 0;
                else
                    currentArmor -= item.itemAttributes[i].attributeValue;
            }
            if (item.itemAttributes[i].attributeName == "Damage")
            {
                if ((currentDamage - item.itemAttributes[i].attributeValue) < 0)
                    currentDamage = 0;
                else
                    currentDamage -= item.itemAttributes[i].attributeValue;
            }
        }
        if (HPMANACanvas != null)
        {
            UpdateManaBar();
            UpdateHPBar();
            updateDamage();
            updateArmor();
        }
    }

    public float GetHP()
    {
        return currentHealth;
    }

    public void SetHP(float hp)
    {
        currentHealth = hp;
    }

    void updateItemEquip(Item item)
    {
        for (int i = 0; i < item.itemAttributes.Count; i++)
        {
            //if (item.itemAttributes[i].attributeName == "Health")
            //{
            //    if ((currentHealth + item.itemAttributes[i].attributeValue) > maxHealth)
            //        currentHealth = maxHealth;
            //    else
            //        currentHealth += item.itemAttributes[i].attributeValue;
            //}
            //if (item.itemAttributes[i].attributeName == "Mana")
            //{
            //    if ((currentMana + item.itemAttributes[i].attributeValue) > maxMana)
            //        currentMana = maxMana;
            //    else
            //        currentMana += item.itemAttributes[i].attributeValue;
            //}
            if (item.itemAttributes[i].attributeName == "Armor")
            {
                //if ((currentArmor + item.itemAttributes[i].attributeValue) > maxArmor)
                //    currentArmor = maxArmor;
                //else
                currentArmor += item.itemAttributes[i].attributeValue;
            }
            if (item.itemAttributes[i].attributeName == "Damage")
            {
                //if ((currentDamage + item.itemAttributes[i].attributeValue) > maxDamage)
                //    currentDamage = maxDamage;
                //else
                currentDamage += item.itemAttributes[i].attributeValue;
            }
        }
        if (HPMANACanvas != null)
        {
            UpdateManaBar();
            UpdateHPBar();
            updateDamage();
            updateArmor();
        }
    }

    void OnBackpack(Item item)
    {
        if (item.itemType == ItemType.Backpack)
        {
            for (int i = 0; i < item.itemAttributes.Count; i++)
            {
                if (mainInventory == null)
                    mainInventory = inventory.GetComponent<Inventory>();
                mainInventory.sortItems();
                if (item.itemAttributes[i].attributeName == "Slots")
                    changeInventorySize(item.itemAttributes[i].attributeValue);
            }
        }
    }

    void UnEquipBackpack(Item item)
    {
        if (item.itemType == ItemType.Backpack)
            changeInventorySize(normalSize);
    }

    void changeInventorySize(int size)
    {
        dropTheRestItems(size);

        if (mainInventory == null)
            mainInventory = inventory.GetComponent<Inventory>();
        if (size == 3)
        {
            mainInventory.width = 3;
            mainInventory.height = 1;
            mainInventory.updateSlotAmount();
            mainInventory.adjustInventorySize();
        }
        if (size == 6)
        {
            mainInventory.width = 3;
            mainInventory.height = 2;
            mainInventory.updateSlotAmount();
            mainInventory.adjustInventorySize();
        }
        else if (size == 12)
        {
            mainInventory.width = 4;
            mainInventory.height = 3;
            mainInventory.updateSlotAmount();
            mainInventory.adjustInventorySize();
        }
        else if (size == 16)
        {
            mainInventory.width = 4;
            mainInventory.height = 4;
            mainInventory.updateSlotAmount();
            mainInventory.adjustInventorySize();
        }
        else if (size == 24)
        {
            mainInventory.width = 6;
            mainInventory.height = 4;
            mainInventory.updateSlotAmount();
            mainInventory.adjustInventorySize();
        }
    }

    void dropTheRestItems(int size)
    {
        if (size < mainInventory.ItemsInInventory.Count)
        {
            for (int i = size; i < mainInventory.ItemsInInventory.Count; i++)
            {
                GameObject dropItem = (GameObject)Instantiate(mainInventory.ItemsInInventory[i].itemModel);
                dropItem.AddComponent<PickUpItem>();
                dropItem.GetComponent<PickUpItem>().item = mainInventory.ItemsInInventory[i];
                dropItem.transform.localPosition = GameObject.FindGameObjectWithTag("Player").transform.localPosition;
            }
        }
    }

    void Start()
    {
        if (HPMANACanvas != null)
        {

            hpText = HPMANACanvas.transform.GetChild(2).GetChild(0).GetComponent<Text>();

            manaText = HPMANACanvas.transform.GetChild(3).GetChild(0).GetComponent<Text>();

            hpImage = HPMANACanvas.transform.GetChild(2).GetComponent<Image>();
            manaImage = HPMANACanvas.transform.GetChild(3).GetComponent<Image>();

            damageText = HPMANACanvas.transform.GetChild(4).GetChild(0).GetComponent<Text>();
            ArmorText = HPMANACanvas.transform.GetChild(5).GetChild(0).GetComponent<Text>();

            UpdateHPBar();
            UpdateManaBar();
            updateDamage();
            updateArmor();
        }



        if (inputManagerDatabase == null)
            inputManagerDatabase = (InputManager)Resources.Load("InputManager");

        if (craftSystem != null)
            cS = craftSystem.GetComponent<CraftSystem>();

        if (GameObject.FindGameObjectWithTag("Tooltip") != null)
            toolTip = GameObject.FindGameObjectWithTag("Tooltip").GetComponent<Tooltip>();
        if (inventory != null)
            mainInventory = inventory.GetComponent<Inventory>();
        if (characterSystem != null)
            characterSystemInventory = characterSystem.GetComponent<Inventory>();
        if (craftSystem != null)
            craftSystemInventory = craftSystem.GetComponent<Inventory>();
    }

	protected void UpdateHPBar()
    {
        hpText.text = (currentHealth.ToString() + "/" + maxHealth);
        float fillAmount = currentHealth / maxHealth;
        hpImage.fillAmount = fillAmount;
    }

	protected void UpdateManaBar()
    {
        manaText.text = (currentMana + "/" + maxMana);
        float fillAmount = currentMana / maxMana;
        manaImage.fillAmount = fillAmount;
    }

	protected void updateDamage()
    {
        damageText.text = currentDamage + "";

    }

	protected void updateArmor()
    {
        ArmorText.text = currentArmor + "";
    }

    public void OnConsumeItem(Item item)
    {
        for (int i = 0; i < item.itemAttributes.Count; i++)
        {
            if (item.itemAttributes[i].attributeName == "Health")
            {
                if ((currentHealth + item.itemAttributes[i].attributeValue) > maxHealth)
                    currentHealth = maxHealth;
                else
                    currentHealth += item.itemAttributes[i].attributeValue;
            }
            if (item.itemAttributes[i].attributeName == "Mana")
            {
                if ((currentMana + item.itemAttributes[i].attributeValue) > maxMana)
                    currentMana = maxMana;
                else
                    currentMana += item.itemAttributes[i].attributeValue;
            }
            if (item.itemAttributes[i].attributeName == "Armor")
            {
                if ((currentArmor + item.itemAttributes[i].attributeValue) > maxArmor)
                    currentArmor = maxArmor;
                else
                    currentArmor += item.itemAttributes[i].attributeValue;
            }
            if (item.itemAttributes[i].attributeName == "Damage")
            {
                if ((currentDamage + item.itemAttributes[i].attributeValue) > maxDamage)
                    currentDamage = maxDamage;
                else
                    currentDamage += item.itemAttributes[i].attributeValue;
            }
        }
        if (HPMANACanvas != null)
        {
            UpdateManaBar();
            UpdateHPBar();
            updateDamage();
            updateArmor();
        }
    }
    public void OnGearItem(Item item)
    {
        for (int i = 0; i < item.itemAttributes.Count; i++)
        {
            if (item.itemAttributes[i].attributeName == "Health")
                maxHealth += item.itemAttributes[i].attributeValue;
            if (item.itemAttributes[i].attributeName == "Mana")
                maxMana += item.itemAttributes[i].attributeValue;
            if (item.itemAttributes[i].attributeName == "Armor")
                maxArmor += item.itemAttributes[i].attributeValue;
            if (item.itemAttributes[i].attributeName == "Damage")
                maxDamage += item.itemAttributes[i].attributeValue;
        }
        if (HPMANACanvas != null)
        {
            UpdateManaBar();
            UpdateHPBar();
            updateDamage();
            updateArmor();
        }
    }

    public void OnUnEquipItem(Item item)
    {
        for (int i = 0; i < item.itemAttributes.Count; i++)
        {
            if (item.itemAttributes[i].attributeName == "Health")
                maxHealth -= item.itemAttributes[i].attributeValue;
            if (item.itemAttributes[i].attributeName == "Mana")
                maxMana -= item.itemAttributes[i].attributeValue;
            if (item.itemAttributes[i].attributeName == "Armor")
                maxArmor -= item.itemAttributes[i].attributeValue;
            if (item.itemAttributes[i].attributeName == "Damage")
                maxDamage -= item.itemAttributes[i].attributeValue;
        }
        if (HPMANACanvas != null)
        {
            UpdateManaBar();
            UpdateHPBar();
            updateDamage();
            updateArmor();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "BeAttacked")
        {
            currentHealth -= 5;
            UpdateHPBar();
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(inputManagerDatabase.CharacterSystemKeyCode))
        {
            if (!characterSystem.activeSelf)
            {
                characterSystemInventory.openInventory();
            }
            else
            {
                if (toolTip != null)
                    toolTip.deactivateTooltip();
                characterSystemInventory.closeInventory();
            }
        }

        if (Input.GetKeyDown(inputManagerDatabase.InventoryKeyCode))
        {
            if (!inventory.activeSelf)
            {
                mainInventory.openInventory();
            }
            else
            {
                if (toolTip != null)
                    toolTip.deactivateTooltip();
                mainInventory.closeInventory();
            }
        }

        if (Input.GetKeyDown(inputManagerDatabase.CraftSystemKeyCode))
        {
            if (!craftSystem.activeSelf)
                craftSystemInventory.openInventory();
            else
            {
                if (cS != null)
                    cS.backToInventory();
                if (toolTip != null)
                    toolTip.deactivateTooltip();
                craftSystemInventory.closeInventory();
            }
        }
    }

}

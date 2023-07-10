using UnityEngine;

[CreateAssetMenu(fileName = "User", menuName = "Inventories/User")]
public class UserInventory : ScriptableObject
{
    public string email;

    public string username;

    public Texture2D picture;

    public string bio;
    
    public string firstName;

    public string lastName;

    public string Email
    {
        set
        {
            if (email != value)
            {
                email = value;
                ExecuteInventoryTrigger();
            }
        }
    }

    public string Username
    {
        set
        {
            if (username != value)
            {
                username = value;
                ExecuteInventoryTrigger();
            }
        }
    }

    public Texture2D Picture
    {
        set
        {
            if (picture != value)
            {
                picture = value;
                ExecuteInventoryTrigger();
            }
        }
    }

    public string Bio
    {
        set
        {
            if (bio != value)
            {
                bio = value;
                ExecuteInventoryTrigger();
            }
        }
    }

    public string FirstName
    {
        set
        {
            if (firstName != value)
            {
                firstName = value;
                ExecuteInventoryTrigger();
            }
        }
    }

    public string LastName
    {
        set
        {
            if (lastName != value)
            {
                lastName = value;
                ExecuteInventoryTrigger();
            }
        }
    }

    public void Init()
    {
        email = string.Empty;

        username = string.Empty;

        picture = null;

        bio = string.Empty;

        firstName = string.Empty;

        lastName = string.Empty;
    }

    public delegate void InventoryTriggeredEventHandler();
    public event InventoryTriggeredEventHandler InventoryTriggered;

    private void ExecuteInventoryTrigger()
    {
        InventoryTriggered?.Invoke();
    }
}
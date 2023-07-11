using UnityEngine;

public class UserInventoryDTO
{
    public string Email { get; set; }

    public string Username { get; set; }

    public Texture2D Picture { get; set; }

    public string Bio { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }
}

[CreateAssetMenu(fileName = "User", menuName = "Inventories/User")]
public class UserInventory : ScriptableObject
{
    public string email;

    public string username;

    public Texture2D picture;

    public string bio;
    
    public string firstName;

    public string lastName;

    public void UpdateUser(UserInventoryDTO user)
    {
        if (user.Email != null)
        {
            email = user.Email;
        }

        if (user.Username != null)
        {
            username = user.Username;
        }

        if (user.Picture != null)
        {
            picture = user.Picture;
        }

        if (user.Bio != null)
        {
            bio = user.Bio;
        }

        if (user.FirstName != null)
        {
            firstName = user.FirstName;
        }

        if (user.LastName != null)
        {
            lastName = user.LastName;
        }
    }

    public void UpdateUserThenNotify(UserInventoryDTO user)
    {
        UpdateUser(user);

        ExecuteInventoryTrigger();
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
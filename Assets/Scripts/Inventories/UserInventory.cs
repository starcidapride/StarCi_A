using Newtonsoft.Json;
using UnityEngine;
using static UserDto;

[CreateAssetMenu(fileName = "User", menuName = "Inventories/User")]
public class UserInventory : ScriptableObject
{
    public string Email { get; set; }

    public string Username { get; set; }

    public Texture2D Picture { get; set; }

    public string Bio { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public DeckCollection DeckCollection { get; set; }

    public void UpdateUser(User user)
    {
        if (user.Email != null)
            Email = user.Email;

        if (user.Username != null)
            Username = user.Username;

        if (user.Picture != null)
            Picture = user.Picture;

        if (user.Bio != null)
            Bio = user.Bio;

        if (user.FirstName != null)
            FirstName = user.FirstName;

        if (user.LastName != null)
            LastName = user.LastName;

        if (user.DeckCollection != null)
            DeckCollection = user.DeckCollection;
    }

    public void UpdateUserThenNotify(User user)
    {
        UpdateUser(user);

        ExecuteInventoryTrigger();
    }

    public void Init()
    {
        Email = string.Empty;

        Username = string.Empty;

        Picture = null;

        Bio = string.Empty;

        FirstName = string.Empty;

        LastName = string.Empty;

        DeckCollection = null;

    }

    public delegate void InventoryTriggeredEventHandler();
    public event InventoryTriggeredEventHandler InventoryTriggered;

    private void ExecuteInventoryTrigger()
    {
        InventoryTriggered?.Invoke();
    }
}
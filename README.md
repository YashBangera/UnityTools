# Scriptable Object Framework

A comprehensive Unity framework that leverages ScriptableObjects to create modular, decoupled, and maintainable game architecture. This framework provides a robust event system, data management solutions, and editor tools to enhance your Unity development workflow.

## Table of Contents
- [Overview](#overview)
- [Installation](#installation)
- [Features](#features)
  - [Value ScriptableObjects](#value-scriptableobjects)
  - [Collection ScriptableObjects](#collection-scriptableobjects)
  - [Event System](#event-system)
  - [Reference System](#reference-system)
  - [Localization System](#localization-system)
- [Usage Examples](#usage-examples)
- [Editor Tools](#editor-tools)
- [Best Practices](#best-practices)
- [API Reference](#api-reference)
- [Contributing](#contributing)
- [License](#license)

## Overview

The Scriptable Object Framework provides a set of tools and patterns that help you:
- **Decouple** your game systems for better modularity
- **Share data** between scenes and GameObjects without direct references
- **Create designer-friendly** systems that can be modified without code changes
- **Debug easier** with runtime value inspection and event logging
- **Build scalable** architectures that grow with your project

## Installation

### Via Unity Package Manager (Recommended)

1. Open your Unity project
2. Navigate to your project's `Packages/manifest.json` file
3. Add the following line to the `dependencies` section:
   ```json
   "com.ybdev.unitytools": "https://github.com/YashBangera/UnityTools.git#0.0.1"
   ```
4. Save the file and Unity will automatically import the package

### Manual Installation

1. Clone or download this repository
2. Copy the `Packages/ScriptableObjectFramework` folder into your project's `Packages` folder

### Compatibility
- Unity 2019.4 or higher
- No external dependencies

## Features

### Value ScriptableObjects

Value ScriptableObjects allow you to create shared data containers that can be referenced by multiple systems without direct coupling.

#### Available Types
- `IntValueSO` - Integer values
- `FloatValueSO` - Float values
- `BoolValueSO` - Boolean values
- `StringValueSO` - String values
- `Vector3ValueSO` - Vector3 values
- `GameObjectValueSO` - GameObject references
- `Texture2DValueSO` - Texture2D references

#### Creating Value ScriptableObjects
1. Right-click in Project window
2. Navigate to `Create > UnityTools > Values > [Type]ValueSO`
3. Name your asset and configure default values

#### Usage Example
```csharp
using UnityEngine;
using UnityTools.ScriptableObjects;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private IntValueSO playerHealthSO;
    [SerializeField] private IntValueSO maxHealthSO;
    
    private void Start()
    {
        // Set initial health
        playerHealthSO.SetValue(maxHealthSO.GetValue());
        
        // Subscribe to health changes
        playerHealthSO.OnValueChanged.AddListener(OnHealthChanged);
    }
    
    private void OnHealthChanged(int newHealth)
    {
        Debug.Log($"Health changed to: {newHealth}");
        if (newHealth <= 0)
        {
            // Handle player death
        }
    }
    
    public void TakeDamage(int damage)
    {
        int currentHealth = playerHealthSO.GetValue();
        playerHealthSO.SetValue(currentHealth - damage);
    }
}
```

### Collection ScriptableObjects

Collections provide dynamic lists of values that can be modified at runtime, perfect for managing groups of objects or data.

#### Available Collection Types
- `IntCollectionSO`
- `FloatCollectionSO`
- `BoolCollectionSO`
- `StringCollectionSO`
- `GameObjectCollectionSO`
- `Texture2DCollectionSO`

#### Usage Example
```csharp
using UnityEngine;
using UnityTools.ScriptableObjects;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObjectCollectionSO activeEnemies;
    
    private void OnEnable()
    {
        // Add this enemy to the active list
        activeEnemies.Add(gameObject);
    }
    
    private void OnDisable()
    {
        // Remove from active list when destroyed
        activeEnemies.Remove(gameObject);
    }
}

// In another script, iterate through all active enemies
public class EnemyRadar : MonoBehaviour
{
    [SerializeField] private GameObjectCollectionSO activeEnemies;
    
    private void Update()
    {
        foreach (GameObject enemy in activeEnemies.GetCollection())
        {
            // Process each active enemy
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            // Update radar display...
        }
    }
}
```

### Event System

The event system provides a decoupled way to communicate between systems using ScriptableObject-based events.

#### Components
- `EventSO` - ScriptableObject event that can be raised with parameters
- `EventListener` - MonoBehaviour component that listens to EventSO and invokes UnityEvents
- `CustomArgs` - Container for passing multiple parameters of any type with events

#### Creating Events
1. Right-click in Project window
2. Navigate to `Create > UnityTools > Events > EventSO`
3. Name your event descriptively (e.g., "OnPlayerDied", "OnLevelCompleted")

#### Raising Events
```csharp
// Simple event raising with parameters
using UnityEngine;
using UnityTools.ScriptableObjects;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private EventSO onPlayerDamaged;
    [SerializeField] private EventSO onPlayerDied;
    
    public void TakeDamage(int damage, GameObject source)
    {
        // Simply pass parameters directly - EventSO creates CustomArgs internally
        onPlayerDamaged.Raise(damage, source, transform.position, Time.time);
        
        if (health <= 0)
        {
            onPlayerDied.Raise(); // Can also raise without parameters
        }
    }
}

// Listening to events programmatically
public class GameManager : MonoBehaviour
{
    [SerializeField] private EventSO onPlayerDamaged;
    
    private void OnEnable()
    {
        onPlayerDamaged.OnRaised += HandlePlayerDamaged;
    }
    
    private void OnDisable()
    {
        onPlayerDamaged.OnRaised -= HandlePlayerDamaged;
    }
    
    private void HandlePlayerDamaged(CustomArgs args)
    {
        // Extract parameters using GetArg<T> with index
        int damage = args.GetArg<int>(0);
        GameObject source = args.GetArg<GameObject>(1);
        Vector3 position = args.GetArg<Vector3>(2);
        float timestamp = args.GetArg<float>(3);
        
        Debug.Log($"Player took {damage} damage from {source.name} at {position} (Time: {timestamp})");
    }
}
```

#### Using EventListener Component

The EventListener component provides a no-code way to respond to events directly in the Unity Inspector.

**Setup Steps:**

1. **Add EventListener Component**
   - Select a GameObject in your scene
   - Add Component → UnityTools → Event Listener
   - The component displays a list of events to monitor

2. **Configure Events in Inspector**
   - Click the '+' button to add a new event listener
   - In the "Channel" field, assign the EventSO you want to listen to
   - Under "On Event Raised", configure the UnityEvent response:
     - Click '+' to add a response
     - Drag the GameObject with your response script
     - Select a method that accepts CustomArgs parameter

3. **Create Response Methods**
   ```csharp
   public class UIManager : MonoBehaviour
   {
       [SerializeField] private Text damageText;
       [SerializeField] private Image healthBar;
       
       // Method signature must accept CustomArgs
       public void OnPlayerDamaged(CustomArgs args)
       {
           if (args.GetParamsCount() > 0)
           {
               int damage = args.GetArg<int>(0);
               GameObject source = args.GetArg<GameObject>(1);
               
               // Update UI
               damageText.text = $"-{damage}";
               StartCoroutine(FadeDamageText());
               
               // Log for debugging
               Debug.Log($"UI: Player damaged by {source?.name ?? "unknown"}");
           }
       }
       
       public void OnEnemyDefeated(CustomArgs args)
       {
           string enemyName = args.GetArg<string>(0);
           int points = args.GetArg<int>(1);
           
           ShowNotification($"{enemyName} defeated! +{points} points");
       }
   }
   ```

4. **Advanced Usage**
   - EventListener can monitor multiple events simultaneously
   - Each event can trigger multiple methods
   - Events are automatically subscribed when GameObject is enabled
   - Events are automatically unsubscribed when GameObject is disabled
   - Perfect for UI responses, sound effects, particle effects, etc.

### Reference System

The reference system allows you to share references to scene objects across different systems without direct coupling.

#### Components
- `TransformReference` - ScriptableObject that holds a Transform reference
- `AssignTransformReference` - Component that assigns its Transform to a TransformReference

#### Usage Example
```csharp
// Automatically assign player transform to a reference
public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private TransformReference playerTransformRef;
    [SerializeField] private GameObject playerPrefab;
    
    private void Start()
    {
        GameObject player = Instantiate(playerPrefab);
        
        // Add component to automatically update the reference
        var assignRef = player.AddComponent<AssignTransformReference>();
        assignRef.SetReference(playerTransformRef);
    }
}

// Use the reference in another system
public class CameraFollow : MonoBehaviour
{
    [SerializeField] private TransformReference playerTransformRef;
    [SerializeField] private Vector3 offset = new Vector3(0, 5, -10);
    
    private void LateUpdate()
    {
        if (playerTransformRef.Value != null)
        {
            transform.position = playerTransformRef.Value.position + offset;
        }
    }
}
```

### Localization System

Simple localization system using ScriptableObjects for managing multilingual text.

#### Components
- `LocalizedString` - ScriptableObject containing localized text data
- `LocalizedTextMeshProUGUI` - Component that automatically updates TextMeshPro text

#### Setting Up Localization
1. Create LocalizedString assets for each text element
2. Configure translations in the inspector
3. Use LocalizedTextMeshProUGUI component on UI elements

#### Usage Example
```csharp
// Create localized UI
using UnityEngine;
using TMPro;
using UnityTools.ScriptableObjects;

public class LocalizedButton : MonoBehaviour
{
    [SerializeField] private LocalizedString buttonText;
    private TextMeshProUGUI textComponent;
    
    private void Start()
    {
        textComponent = GetComponentInChildren<TextMeshProUGUI>();
        UpdateText();
    }
    
    private void UpdateText()
    {
        if (buttonText != null && textComponent != null)
        {
            textComponent.text = buttonText.GetLocalizedText();
        }
    }
}
```

## Editor Tools

### Custom Inspectors
- **Value SO Inspector**: Runtime value editing and monitoring
- **Collection SO Inspector**: View and modify collections in play mode
- **Event SO Inspector**: Event debugging with raise button and event history

### ScriptableObject Reference Finder
Find all references to a ScriptableObject in your project:
1. Right-click on any ScriptableObject
2. Select `UnityTools > Find References`
3. View all assets and scenes using this ScriptableObject

### Localization Editor Window
Manage all localized strings from a central location:
1. Open via `Window > UnityTools > Localization Editor`
2. View and edit all LocalizedString assets
3. Add/remove language support
4. Export/import for translation

### Creation Wizards
Quick creation tools for common patterns:
- **Localized Text Wizard**: Creates LocalizedString and configures UI in one step
- **Localized Button Wizard**: Sets up complete localized button with events

## Best Practices

### 1. Naming Conventions
- **Events**: Use verb phrases (OnPlayerDied, OnLevelStarted)
- **Values**: Use descriptive names (PlayerHealth, MaxEnemyCount)
- **Collections**: Use plural names (ActiveEnemies, CollectedItems)

### 2. Organization
```
Assets/
├── ScriptableObjects/
│   ├── Events/
│   │   ├── Player/
│   │   ├── UI/
│   │   └── Gameplay/
│   ├── Values/
│   │   ├── Player/
│   │   ├── Game/
│   │   └── Settings/
│   └── Collections/
```

### 3. Performance Considerations
- Value SOs are lightweight and efficient
- Avoid creating SOs at runtime (use object pooling if needed)
- Collections have O(n) removal - use dictionaries for large datasets

### 4. Debugging Tips
- Enable "Print Logs" on EventSO for console output
- Use the event history in EventSO inspector
- Values show current and default in inspector
- Collections display size and contents in play mode

## API Reference

### BaseValueSO<T>
```csharp
public T GetValue()                    // Get current value
public void SetValue(T value)          // Set value (triggers OnValueChanged)
public void ResetToDefault()           // Reset to default value
public UnityEvent<T> OnValueChanged    // Event fired when value changes
```

### BaseCollectionSO<T>
```csharp
public void Add(T item)                // Add item to collection
public void Remove(T item)             // Remove item from collection
public bool Contains(T item)           // Check if item exists
public List<T> GetCollection()         // Get full collection
public void Clear()                    // Clear all items
public int Count                       // Number of items
```

### EventSO
```csharp
public void Raise(params object[] i_params) // Raise event with any number of parameters
public UnityAction<CustomArgs> OnRaised     // Subscribe to event programmatically
```

### CustomArgs
```csharp
new CustomArgs(params object[] i_params)    // Constructor (created automatically by EventSO)
public T GetArg<T>(int i_index = 0)         // Get typed parameter at index (default: 0)
public int GetParamsCount()                 // Get total number of parameters
public object[] GetParams()                 // Get all parameters as array
public object GetParamObject(int index)     // Get parameter as object at index
```

### TransformReference
```csharp
public Transform Value { get; set; }   // Get/set transform reference
public bool HasValue()                 // Check if reference is assigned
```

## Contributing

We welcome contributions! Please follow these guidelines:

### Creating Issues
1. Check existing issues first
2. Use issue templates when available
3. Provide clear reproduction steps
4. Include Unity version and package version

### Pull Requests
1. Fork the repository
2. Create a feature branch
3. Follow existing code style
4. Add unit tests for new features
5. Update documentation
6. Submit PR with clear description

### Code Style
- Use clear, descriptive names
- Add XML documentation to public methods
- Follow Unity C# conventions
- Keep it simple and maintainable

## License

This project is licensed under the [MIT License](LICENSE). You are free to use, modify, and distribute this framework in your projects.
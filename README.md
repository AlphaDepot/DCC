# DCC

## Developer Code Cleaner

Ever tried to back up your codebase, only to realize you’re actually copying a small mountain of `node_modules`, `bin`,
`obj`, and other digital junk? Hours later, your USB drive is full, your patience is gone, and you’re still not sure why
you need 2GB of dependencies for a "Hello World" app.

**Enter DCC – Developer Code Cleaner!**

DCC is your friendly neighborhood code janitor. It sweeps away all those pesky build artifacts, dependency folders, and
other clutter that sneak into your projects over time. With a single click, DCC finds and removes:

- `node_modules`
- `bin` and `obj`
- `dist`, `build`, and more

No more endless waiting, no more wasted space, and no more "Why is this folder so big?" moments. DCC leaves you with
just the files you actually care about—ready to zip, move, or share.

## How to use DCC

1. Download and install the app from the Releases section.
2. Launch DCC.
3. Click the **plus (+) Add** button in the upper left corner.
4. Fill out the form:
    - **Name:** e.g., "JavaScript", "CSharp", or something specific like "React" or "Angular".
    - **Description:** A brief note about what this cleaner does, like "Cleans up JavaScript projects". **(Optional)**
    - **Location:** Select the root folder where your projects live. DCC will scan all subfolders here.
    - **Directories to Clean:** Enter the names of folders you want DCC to remove, like node_modules, bin, obj, dist,
      build, .next, etc. Type each folder name as a separate string in the list—you can add as many as you need.
5. Click **Add Cleaner** to save your settings.
6. Hit the **Cleaner** button (the green broom icon) to start cleaning.
7. Relax and watch your codebase get squeaky clean!

**Note:** You can create multiple cleaners for different project types, or set up a global cleaner that applies to all your projects. Each cleaner has its own cleaning button, allowing you to clean specific project types individually. Alternatively, use the broom icon next to the add button to clean all projects at once, no matter how many cleaners you have. This gives you flexibility and better control over your cleaning process.

## Exporting and Importing Cleaners

You can export your cleaner configurations with the down arrow and import them with the up arrow found on the right side of the app bar. This feature is helpful for sharing your setup or backing up your settings.

**Note:** Configuration files are not directly interchangeable between Windows and macOS. You must manually update the `Location` paths to match your operating system, or errors will occur. Configuration files use JSON format, as shown below.

### Example Configuration File (Windows)
```json
{
   "Cleaners": [
      {
         "Id": 1,
         "Name": "Javascript",
         "Description": "Cleanup javascript code",
         "Directories": [
            "node_modules"
         ],
         "Location": "C:\\Users\\LST\\Development\\Code\\Javascript"
      },
      {
         "Id": 2,
         "Name": "CSharp",
         "Description": "Cleanup C# code",
         "Directories": [
            "bin",
            "obj"
         ],
         "Location": "C:\\Users\\LST\\Development\\Code\\CSharp"
      }
   ]
}
```

### Example Configuration File (macOS)
```json
{
   "Cleaners": [
      {
         "Id": 1,
         "Name": "Javascript",
         "Description": "Cleanup javascript code",
         "Directories": [
            "node_modules",
            "dist"
         ],
         "Location": "/Volumes/Code/Development/Javascript"
      },
      {
         "Id": 2,
         "Name": "CSharp",
         "Description": "Cleanup C# code",
         "Directories": [
            "bin",
            "obj"
         ],
         "Location": "/Volumes/Code/Development/CSharp"
      }
   ]
}   
```
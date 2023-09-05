# Custom Unity Tools & Frameworks

Welcome to the Custom Unity Tools & Frameworks open-source project! This repository houses a collection of custom Unity tools that extend the Unity editor's functionality and provide frameworks for development. Whether you're a beginner or an experienced Unity developer, these tools and frameworks aim to simplify your workflow and boost your productivity.

## Table of Contents
- [Getting Started with the Custom Unity Package via Unity Package Manager](#getting-started-with-the-custom-unity-package-via-unity-package-manager)
- [Cloning the Repository for Editing in Unity](#cloning-the-repository-for-editing-in-unity)
- [Features](#features)
- [Usage](#usage)
- [Contributing](#contributing)
- [License](#license)

## Getting Started with the Custom Unity Package via Unity Package Manager

To get started with this custom Unity package using Unity Package Manager (UPM), follow these steps:

1. **Open Your Unity Project:**
   - Launch Unity and open your existing project or create a new one if needed.

2. **Locate the `manifest.json` File:**
   - In your Unity project's directory, locate the `manifest.json` file. This file is typically found at the following path:
     ```
     YourUnityProject/Packages/manifest.json
     ```

3. **Edit the `manifest.json` File:**
   - Open the `manifest.json` file in a text editor of your choice.

4. **Add the Package Dependency:**
   - Inside the `dependencies` section of the `manifest.json` file, add a new line with the URL to your custom package. The URL can be in one of the following formats:
     - **Git URL:**
       ```
       "com.ybdev.unitytools": "https://github.com/YashBangera/UnityTools.git"
       ```
     - **Local Path:**
       ```
       "com.ybdev.unitytools": "file:../relative/path/to/your/package"
       ```
     Replace `"com.example.custompackage"` with the desired package name and URL with the URL to your package.

5. **Save the `manifest.json` File:**
   - Save the changes to the `manifest.json` file.

6. **Unity Package Manager Auto-resolution:**
   - Unity Package Manager will automatically detect and resolve the package dependency. It will download the package from the specified URL or local path.

7. **Verify Package Import:**
   - Once Unity has successfully imported the package, you should see it listed in the "Packages" section of the Unity editor.

Now, you can start using the custom Unity package within your project.

## Cloning the Repository for Editing in Unity

If you wish to make edits or contribute to this custom Unity package, you'll need to clone the repository to your local development environment. Follow these steps to clone the repository:

1. **Install Git:**
   - If you don't already have Git installed, you can download and install it from the official website: [Git Downloads](https://git-scm.com/downloads).

2. **Clone the Repository:**
   - Open a terminal or command prompt and navigate to the directory where you want to store the project. Then, run the following command to clone the repository:
     ```bash
     git clone https://github.com/your-username/your-repository.git
     ```
     Replace `your-username` and `your-repository` with your actual GitHub username and the name of your repository.

3. **Access the Cloned Repository:**
   - Navigate into the cloned repository using the `cd` command:
     ```bash
     cd your-repository
     ```
     Now, you are inside the project directory and ready to make changes.

4. **Make Edits and Contributions:**
   - You can now make changes to the package's source code, documentation, or any other relevant files.

5. **Commit Your Changes:**
   - After making changes, commit them using Git. Run the following commands within the repository directory:
     ```bash
     git add .
     git commit -m "Describe your changes here"
     ```
     Replace "Describe your changes here" with a brief description of the changes you've made.

6. **Push Changes:**
   - If you have write access to the repository (i.e., it's your repository), you can push your changes back to the remote repository on GitHub.

7. **Create a Pull Request:**
   - If you want to contribute your changes to the original repository, create a pull request on GitHub. This allows the repository maintainers to review and merge your changes. Visit your repository's GitHub page, and you should see an option to create a pull request.

## Features
WIP

## Usage
WIP

## Contributing
Creating an Issue

To create an issue:

Navigate to the Issues Page:

1. **Go to the "Issues" tab on the GitHub repository's page.**
   -Click on "New Issue":
   -Click the green "New Issue" button.
2. **Fill in the Details:**
   -Provide all the required information following the guidelines mentioned above.
3. **Submit the Issue:**
   -After filling in the details, click the "Submit new issue" button to create the issue.
4. **Engage in Discussion:**
   -After creating the issue, engage in any further discussion with maintainers and contributors regarding the problem. Be responsive to requests for additional information or clarification.

## License
This project is licensed under the [MIT License](LICENSE). You are free to use, modify, and distribute the code according to the terms of the MIT License.

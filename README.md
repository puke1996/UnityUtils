# Puke's Unity Utils

## Documentation

As there are many steps to developing a Unity package, the documentation is currently split into a couple of parts, each covering a specific feature or overview:

- [File and Folder Structure](https://omiyagames.github.io/template-unity-package/manual/structure.html)
- [Customizing Package Files](https://omiyagames.github.io/template-unity-package/manual/customizePackage.html)
- [Adding Source Code and Assets](https://omiyagames.github.io/template-unity-package/manual/customizeSource.html)
- [Adding Importable Assets](https://omiyagames.github.io/template-unity-package/manual/customizeSamples.html)
- [Customizing Documentation](https://omiyagames.github.io/template-unity-package/manual/customizeDocumentation.html)

The [author](https://github.com/japtar10101) of this package also wrote a blog post on [*How to Split Up an Existing Unity Git Project into Smaller Unity Packages*](https://www.taroomiya.com/2020/04/29/how-to-split-up-an-existing-unity-git-project-into-smaller-unity-packages/).

Finally, changes in the project is documented under the [change log page](https://omiyagames.github.io/template-unity-package/manual/changelog.html).

## Install

For ease of updating installation instructions in future projects, a template instruction is specified below.  Note that [instructions on using OpenUPM's](#through-openupm) to install *this* template package is merely theoretical, as this project is not actually hosted in OpenUPM:

There are two common methods for installing this package.

### Through [Unity Package Manager](https://docs.unity3d.com/Manual/upm-ui-giturl.html)

Unity's own Package Manager supports [importing packages through a URL to a Git repo](https://docs.unity3d.com/Manual/upm-ui-giturl.html):

1. First, on this repository page, click the "Clone or download" button, and copy over this repository's HTTPS URL.  
2. Then click on the + button on the upper-left-hand corner of the Package Manager, select "Add package from git URL..." on the context menu, then paste this repo's URL!

While easier to follow than the first method, this one does not support dependency resolution and package upgrading when a new version is released.  So proceed with caution.

## LICENSE

Overall package is licensed under [MIT](/LICENSE.md), unless otherwise noted in the [3rd party licenses](/THIRD%20PARTY%20NOTICES.md) file and/or source code.

NReflect
========

Overview
--------
NReflect provides an extremly simple way to get a description of all types of an
assembly. Reflection is used to extract the types and every member of each type.
The result of this process is a very simple to use object tree. With its
powerfull filter functionality, one will get only the types or elemts which one
realy needs. After the assembly is reflected, it remains unlocked because
reflection takes place inside another application domain if desired.


Features
--------
- Works with nearly all .NET assemblies
- Extracts all types and members
- Powerful filter to determine what to reflect
- Reflection takes place inside a second application domain so assemblies aren't
  locked afterwards if desired
- Result is a simple to use object tree
- Result object tree can be traversed by a visitor


Requirements
------------
To compile the project, you will need Visual Studio 2010. (It might work with
older versions of Visual Studio if you create a new solution file. But this is
not tested.)


Support
-------
Author: Malte Ried (mried@users.sourceforge.net)
Homepage: http://nreflect.sourceforge.net
SourceForge foundry: http://sourceforge.net/projects/nreflect/

The latest version with full source code is always available at SourceForge.
You can submit bug reports, feature requests or other suggestions on the
SourceForge tracking system, or you can contact the author via e-mail.
Please send your feedback to improve this library.


License
-------
NReflect is licensed under the GNU Lesser General Public License v3.0 (LGPL v3.0).

/* $Id: README 5517 2012-06-22 09:34:19Z naudefj $ */ 

This package contains all the files required to install FUDforum, 
uninstall FUDforum and to upgrade existing forum installations.

Install steps:
--------------

1) Copy 'install.php' and 'fudforum_archive' to a web browsable directory.
	It is a good idea to place the script in the same directory you
	intend to install the forum to.

2) Run install.php from your web browser.

3) The install wizard will take you through 5 simple configuration steps.

Detailed instructions at http://cvs.prohost.org/index.php/Installation


Upgrade steps:
--------------

1) Copy 'upgrade.php' and 'fudforum_archive' to your forum's web directory.
   Tip: Upload them with your forum's 'File Manager' Admin Control Panel.

2) Run upgrade.php from your web browser.

3) After the upgrade, the consistency checker will be started.
   If not, you will need to run the consistency checker manually.

Detailed instructions at http://cvs.prohost.org/index.php/Installation


Uninstall steps:
----------------

1) Open 'uninstall.php' with a text editor and comment out the 2nd line
   of this script.

2) Copy uninstall.php to your forum's Web Root directory.
   Tip: Upload it with your forum's 'File Manager' Admin Control Panel.

3) Run uninstall.php from your web browser.


Command line operations:
------------------------

The above mentioned "install.php", "upgrade.php" and "uninstall.php"
scripts can also run from command line. This will allow automated
and mass deployment of the forum software. Details are available
on the wiki.

Enjoy!

The FUDforum team

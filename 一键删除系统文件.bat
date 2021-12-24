@echo off
set nowPath=%cd%
cd /
cd %nowPath%

::delete specify file(*.pdb,*.vshost.*)
for /r %nowPath% %%i in (*.pdb,*.vshost.*) do (del %%i)

::delete specify folder(obj,bin,.vs)
for /r %nowPath% %%i in (obj,bin,.vs) do (IF EXIST %%i RD /s /q %%i)

echo OK
pause

@echo off
for %%i in (icons\classic\*.svg) do (
echo ^<data name="classic_%%~ni" type="System.Resources.ResXFileRef, System.Windows.Forms"^>
echo   ^<value^>icons\classic\%%~ni.svg;System.Byte[], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089^</value^>
echo ^</data^>
)
for %%i in (icons\high-contrast\*.svg) do (
echo ^<data name="high_contrast_%%~ni" type="System.Resources.ResXFileRef, System.Windows.Forms"^>
echo   ^<value^>icons\high-contrast\%%~ni.svg;System.Byte[], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089^</value^>
echo ^</data^>
)
for %%i in (icons\vivid\*.svg) do (
echo ^<data name="vivid_%%~ni" type="System.Resources.ResXFileRef, System.Windows.Forms"^>
echo   ^<value^>icons\vivid\%%~ni.svg;System.Byte[], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089^</value^>
echo ^</data^>
)

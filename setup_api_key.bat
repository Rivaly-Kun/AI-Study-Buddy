@echo off
echo Setting GEMINI_API_KEY environment variable...
echo.
echo IMPORTANT: This script MUST be run as Administrator!
echo Right-click the file and select "Run as administrator"
echo.
echo If you see "Access to the registry path is denied", you didn't run as admin.
echo.

REM Check for admin rights
net session >nul 2>&1
if %errorLevel% == 0 (
    echo ✅ Running as Administrator
) else (
    echo ❌ ERROR: Not running as Administrator!
    echo Please right-click this file and select "Run as administrator"
    pause
    exit /b 1
)

echo.
echo Current API key (if any):
echo %GEMINI_API_KEY%
echo.

REM Replace YOUR_NEW_API_KEY_HERE with your actual API key
setx GEMINI_API_KEY "GEMINI_API_KEY" /M

if %errorLevel% == 0 (
    echo.
    echo ✅ Environment variable set successfully!
    echo.
    echo 🔄 Please restart your application and any command prompts.
    echo.
    echo To verify it worked, open a NEW command prompt and run:
    echo echo %%GEMINI_API_KEY%%
) else (
    echo.
    echo ❌ Failed to set environment variable!
    echo Make sure you're running as Administrator.
)

echo.
pause
# 🔑 API Key Setup Instructions

## 🚨 IMPORTANT: Your API Key Has Been Compromised

Your Google Gemini API key was hardcoded in the source code and has been reported as leaked by Google. This is a serious security issue that needs to be fixed immediately.

## ✅ What I've Done

1. **Removed hardcoded API keys** from the source code
2. **Implemented secure environment variable usage** for API keys
3. **Created a setup script** to help you configure the new API key

## 🔧 How to Fix This

### Step 1: Generate a New API Key

1. Go to [Google AI Studio](https://makersuite.google.com/app/apikey)
2. Sign in with your Google account
3. Click "Create API Key"
4. Copy the new API key (it will look like: `AIzaSyD...`)

### Step 2: Set the Environment Variable

**Option A: Use the PowerShell Script (Recommended)**

1. Right-click `setup_api_key.ps1` and select "Run with PowerShell" → "Run as administrator"
2. Enter your API key when prompted
3. The script will set it automatically

**Option B: Use the Batch Script**

1. Right-click `setup_api_key.bat` and select "Run as administrator"
2. The script will show if it's running as admin and set the variable

**Option C: Manual Setup**

Open Command Prompt as Administrator and run:
```batch
setx GEMINI_API_KEY "YOUR_ACTUAL_API_KEY_HERE" /M
```

### Step 3: Verify Setup

1. Open a new Command Prompt
2. Run: `echo %GEMINI_API_KEY%`
3. You should see your API key displayed

### Step 4: Test the Application

1. Close any running instances of the app
2. Run `dotnet run` from the project directory
3. The app should now work with your new API key

## 🔒 Security Best Practices

- **Never** hardcode API keys in source code
- **Never** commit API keys to version control
- Use environment variables or secure config files
- Regularly rotate your API keys
- Monitor your API usage for unauthorized access

## 🚫 What NOT to Do

- Don't reuse the old API key (`AIzaSyA3QmEdnbzewFlffYGDu9LfVPrtl2DGSqw`)
- Don't hardcode the new key in the source code
- Don't share your API key with anyone
- Don't commit API keys to Git

## ❓ Troubleshooting

If you get "GEMINI_API_KEY environment variable is not set" error:
1. Make sure you ran the setup script as Administrator (right-click → "Run as administrator")
2. If using batch file, check that it says "✅ Running as Administrator"
3. Restart your computer or open a new command prompt
4. Check that the environment variable is set correctly

If you get "Access to the registry path is denied":
- You didn't run the script as Administrator
- Right-click the script and select "Run as administrator"

If `echo %GEMINI_API_KEY%` shows `%GEMINI_API_KEY%` instead of your key:
- The environment variable wasn't set
- Try running the setup script again as Administrator
- Restart your command prompt or computer

If the API still doesn't work:
1. Verify your new API key is correct
2. Check your Google Cloud billing/quota
3. Make sure the API key has Gemini API access enabled
#Requires -RunAsAdministrator

Write-Host "Setting GEMINI_API_KEY environment variable..." -ForegroundColor Green
Write-Host ""
Write-Host "IMPORTANT: This script must be run as Administrator!" -ForegroundColor Yellow
Write-Host ""

# Check if API key is already set
$currentKey = [Environment]::GetEnvironmentVariable("GEMINI_API_KEY", "Machine")
if ($currentKey) {
    Write-Host "Current API key found: $currentKey" -ForegroundColor Cyan
    $overwrite = Read-Host "Do you want to overwrite it? (y/n)"
    if ($overwrite -ne 'y') {
        Write-Host "Keeping existing API key." -ForegroundColor Yellow
        exit
    }
}

# Get API key from user
$apiKey = Read-Host "Enter your Google Gemini API key"

if ([string]::IsNullOrWhiteSpace($apiKey)) {
    Write-Host "No API key provided. Exiting." -ForegroundColor Red
    exit
}

# Set the environment variable
try {
    [Environment]::SetEnvironmentVariable("GEMINI_API_KEY", $apiKey, "Machine")
    Write-Host ""
    Write-Host "✅ Environment variable set successfully!" -ForegroundColor Green
    Write-Host "API Key: $apiKey" -ForegroundColor Cyan
    Write-Host ""
    Write-Host "Please restart your application and any command prompts." -ForegroundColor Yellow
    Write-Host "You can verify it worked by running: echo `$env:GEMINI_API_KEY" -ForegroundColor Gray
} catch {
    Write-Host "❌ Failed to set environment variable: $($_.Exception.Message)" -ForegroundColor Red
}

Read-Host "Press Enter to exit"
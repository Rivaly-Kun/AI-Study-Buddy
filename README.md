# AI Study Buddy

A comprehensive Windows desktop application designed to enhance your learning experience through interactive study modes, AI-powered assistance, and intelligent note-taking features.

## Features

### 🤖 AI-Powered Chat
- Conversational interface with Google's Gemini 2.5 Flash AI model
- Customizable AI assistant name and personality
- Context-aware responses based on loaded study materials
- Support for PDF document analysis and Q&A

### 📚 Study Modes
- **Quiz Mode**: Interactive quizzes with multiple choice questions
- **Mock Exam Mode**: Timed examinations with difficulty levels
- **Cram Mode**: Focused study sessions with topic generation
- **Focus Mode**: Distraction-free reading and note-taking

### 📝 Advanced Note-Taking
- Text highlighting with customizable colors
- Automatic note saving to database
- Manual note addition from chat conversations
- Eraser tool for removing highlights

### ⚙️ Customization
- Light/Dark theme support with automatic high-contrast detection
- Adjustable font sizes (configurable via slider)
- Customizable AI assistant name (default: "Bud")
- AI response style preferences (Detailed, Concise, etc.)
- Auto-save settings for highlights and notes
- Notification preferences
- Default study mode selection
- All settings persisted to local SQLite database

### 📄 Document Support
- PDF file loading and text extraction
- Multiple PDF management
- Context-aware AI responses based on loaded documents

### 💾 Chat History
- Automatic saving of all conversations to local database
- Session-based chat organization with intelligent title generation
- Browse and reload previous chat sessions
- Delete unwanted chat sessions with confirmation
- Titles automatically generated based on conversation content (e.g., "Quiz/Study Questions", "Math Problems", "Programming Help")
- Persistent chat history across app restarts

## Requirements

- Windows 10 or later
- .NET 8.0 Runtime
- Internet connection for AI features

## 🔑 API Key Setup (Required)

**Important**: This application requires a Google Gemini API key to function. Your API key must be configured as an environment variable.

### Step 1: Get a Google Gemini API Key
1. Visit [Google AI Studio](https://makersuite.google.com/app/apikey)
2. Sign in with your Google account
3. Click "Create API Key"
4. Copy the generated API key

### Step 2: Set Environment Variable
**Option A: Use the provided setup script (Recommended)**
1. Run `setup_api_key.bat` as Administrator
2. Edit the script to replace `YOUR_NEW_API_KEY_HERE` with your actual API key
3. The script will set the environment variable system-wide

**Option B: Manual setup**
Open Command Prompt as Administrator and run:
```batch
setx GEMINI_API_KEY "YOUR_ACTUAL_API_KEY_HERE" /M
```

### Step 3: Verify Setup
Open a new Command Prompt and run:
```batch
echo %GEMINI_API_KEY%
```
You should see your API key displayed.

## Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/Rivaly-Kun/AI-Study-Buddy.git
   cd "AI-Study-Buddy/Ai Study Buddy - Gemini"
   ```

2. Restore dependencies:
   ```bash
   dotnet restore
   ```

3. Build the project:
   ```bash
   dotnet build
   ```

4. Run the application:
   ```bash
   dotnet run
   ```

## Usage

### Getting Started
1. Launch the application
2. Access Settings (⚙️) to customize your experience:
   - Set your preferred AI assistant name
   - Choose theme and font size
   - Configure auto-save and notification preferences

### Study Modes
- **Chat Mode**: Default mode for AI conversations
- **Quiz Mode**: Generate and take quizzes
- **Mock Exam**: Simulate exam conditions with timers
- **Cram Mode**: Load PDFs and generate study topics
- **Focus Mode**: Dedicated reading environment

### Working with Documents
1. Use the Cram card to load PDF files
2. Ask questions about the content in chat mode
3. Highlight important text for automatic note saving
4. Use the eraser tool to remove highlights

### Chat History Management
- All conversations are automatically saved to the database
- Click "💬 Chat History" in the sidebar to browse previous sessions
- Select a session to reload and continue the conversation
- Delete sessions you no longer need with the "Delete Session" button
- Each session is uniquely identified and can be revisited anytime

### Note Management
- Highlights are automatically saved when auto-save is enabled
- Right-click in chat to manually add selections to notes
- Access notes through the database (SQLite)

## Architecture

- **Frontend**: Windows Forms (.NET 8) with custom UI controls
- **AI Integration**: Google Gemini 2.5 Flash API for conversational AI
- **Database**: SQLite with custom DatabaseHelper for notes, settings, and chat history persistence
- **PDF Processing**: PdfPig for text extraction and iText7 for advanced PDF operations
- **Styling**: Custom text formatting, highlighting, and theme system
- **Settings Management**: Centralized AppSettings model with database persistence
- **Chat History**: Session-based conversation storage with intelligent title generation based on content analysis

## Dependencies

- Microsoft.Data.Sqlite.Core
- PdfPig
- iText7
- SQLitePCLRaw.bundle_e_sqlite3
- System.Data.SQLite.EF6

## Contributing

Contributions are welcome! Please feel free to submit pull requests or open issues for bugs and feature requests.

## License

This project is proprietary software. All rights reserved.

---

**Note**: This application uses Google's Gemini AI service. Ensure you have a valid API key configured for full functionality.

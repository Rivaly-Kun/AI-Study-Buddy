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
- Light/Dark theme support
- Adjustable font sizes
- AI response style preferences (Detailed, Concise, etc.)
- Auto-save settings for highlights
- Notification preferences

### 📄 Document Support
- PDF file loading and text extraction
- Multiple PDF management
- Context-aware AI responses based on loaded documents

## Requirements

- Windows 10 or later
- .NET 8.0 Runtime
- Internet connection for AI features

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

### Note Management
- Highlights are automatically saved when auto-save is enabled
- Right-click in chat to manually add selections to notes
- Access notes through the database (SQLite)

## Architecture

- **Frontend**: Windows Forms (.NET 8)
- **AI Integration**: Google Gemini API
- **Database**: SQLite with Entity Framework
- **PDF Processing**: PdfPig and iText7 libraries
- **Styling**: Custom text formatting and highlighting

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

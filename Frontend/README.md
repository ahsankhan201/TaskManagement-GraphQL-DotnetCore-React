# 📋 React Spectrum Task Manager

A beautiful task management application built with React Spectrum and modern React patterns.

## ✨ Features

- **Modern UI**: Built with Adobe React Spectrum components for a professional look
- **API Integration**: Ready to connect with real backend APIs
- **Task Management**: Create, view, and toggle task status between "Pending" and "Completed"
- **Real-time Updates**: Immediate UI feedback with smooth transitions
- **Error Handling**: Comprehensive error handling and loading states
- **Responsive Design**: Works great on all screen sizes
- **Clean Architecture**: Well-organized, maintainable code structure

## 🚀 Getting Started

1. **Install dependencies**:
   ```bash
   npm install
   ```

2. **Start the development server**:
   ```bash
   npm run dev
   ```

3. **Open your browser** and navigate to `http://localhost:5173`

## 🏗️ Architecture

### Components
- **TaskManager**: Main container component with API integration
- **TaskForm**: Form for creating new tasks with API validation
- **TaskList**: Displays all tasks with filtering and statistics
- **TaskItem**: Individual task component with API status updates

### API Integration
- **HTTP Client**: Axios-based API service with interceptors
- **Custom Hooks**: `useTasks` hook for state management
- **Error Handling**: Comprehensive error handling and loading states
- **Configuration**: Centralized API configuration and endpoints

### Key Features
- **API Ready**: Configured to connect with real backend APIs
- **Loading States**: Visual feedback during API operations
- **Error Handling**: User-friendly error messages and retry options
- **Authentication**: Built-in support for JWT tokens

## 🛠️ Tech Stack

- **React 19** - Latest React with concurrent features
- **Adobe React Spectrum** - Professional UI component library
- **Axios** - HTTP client for API communication
- **Vite** - Fast build tool and dev server
- **ESLint** - Code quality and consistency

## 📝 Usage

### API Setup
1. **Configure API**: Update `src/config/apiConfig.js` with your API endpoints
2. **Set Base URL**: Point to your backend API server
3. **Test Connection**: Verify API connectivity and data flow

### Task Management
1. **Create a Task**: Use the "Add New Task" form to create tasks via API
2. **View Tasks**: All tasks are fetched from API and displayed in organized sections
3. **Toggle Status**: Click the status button to update task status via API
4. **Track Progress**: See real-time counts of pending and completed tasks

## 🔧 Development

The project includes:
- ESLint configuration for code quality
- Hot module replacement for fast development
- Clean, maintainable component architecture
- API service layer with error handling
- Custom hooks for state management
- Responsive design patterns

## 📚 API Documentation

For detailed API integration instructions, see [API_SETUP.md](./API_SETUP.md)

## 📦 Available Scripts

- `npm run dev` - Start development server
- `npm run build` - Build for production
- `npm run preview` - Preview production build
- `npm run lint` - Run ESLint
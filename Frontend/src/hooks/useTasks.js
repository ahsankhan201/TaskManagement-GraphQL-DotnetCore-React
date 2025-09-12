import { useState, useEffect, useCallback } from 'react';
import { taskApi } from '../services/taskApi';

export const useTasks = () => {
  const [tasks, setTasks] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  // Fetch all tasks
  const fetchTasks = useCallback(async () => {
    try {
      setLoading(true);
      setError(null);
      const data = await taskApi.getTasks();
      setTasks(data);
    } catch (err) {
      setError(err.message || 'Failed to fetch tasks');
      console.error('Error fetching tasks:', err);
    } finally {
      setLoading(false);
    }
  }, []);

  // Create a new task
  const createTask = useCallback(async (taskData) => {
    try {
      setError(null);
      const newTask = await taskApi.createTask(taskData);
      setTasks(prevTasks => [...prevTasks, newTask]);
      return newTask;
    } catch (err) {
      setError(err.message || 'Failed to create task');
      throw err;
    }
  }, []);

  // Update task status
  const updateTaskStatus = useCallback(async (id, status) => {
    try {
      setError(null);
      console.log('Updating task status:', { id, status });
      const updatedTask = await taskApi.updateTaskStatus(id, status);
      console.log('Updated task received:', updatedTask);
      
      setTasks(prevTasks =>
        prevTasks.map(task => {
          if (task.id === parseInt(id)) {
            console.log('Replacing task:', task, 'with:', updatedTask);
            return updatedTask;
          }
          return task;
        })
      );
      return updatedTask;
    } catch (err) {
      setError(err.message || 'Failed to update task status');
      throw err;
    }
  }, []);

  // Update task
  const updateTask = useCallback(async (id, taskData) => {
    try {
      setError(null);
      const updatedTask = await taskApi.updateTask(id, taskData);
      setTasks(prevTasks =>
        prevTasks.map(task =>
          task.id === id ? updatedTask : task
        )
      );
      return updatedTask;
    } catch (err) {
      setError(err.message || 'Failed to update task');
      throw err;
    }
  }, []);

  // Delete task
  const deleteTask = useCallback(async (id) => {
    try {
      setError(null);
      await taskApi.deleteTask(id);
      setTasks(prevTasks => prevTasks.filter(task => task.id !== id));
    } catch (err) {
      setError(err.message || 'Failed to delete task');
      throw err;
    }
  }, []);

  // Load tasks on component mount
  useEffect(() => {
    fetchTasks();
  }, [fetchTasks]);

  return {
    tasks,
    loading,
    error,
    fetchTasks,
    createTask,
    updateTaskStatus,
    updateTask,
    deleteTask,
  };
};

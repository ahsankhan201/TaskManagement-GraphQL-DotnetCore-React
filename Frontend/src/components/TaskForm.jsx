import React, { useState } from 'react';
import {
  View,
  Flex,
  TextField,
  TextArea,
  Button,
  Heading,
  Divider,
  Well,
} from '@adobe/react-spectrum';

export default function TaskForm({ onAddTask }) {
  const [title, setTitle] = useState('');
  const [description, setDescription] = useState('');
  const [isCreating, setIsCreating] = useState(false);
  const [error, setError] = useState(null);

  const handleSubmit = async () => {
    if (!title.trim()) {
      setError('Please enter a task title');
      return;
    }

    setIsCreating(true);
    setError(null);
    
    try {
      await onAddTask({
        title: title.trim(),
        description: description.trim(),
      });
      
      setTitle('');
      setDescription('');
    } catch (err) {
      setError(err.message || 'Failed to create task');
    } finally {
      setIsCreating(false);
    }
  };

  const handleReset = () => {
    setTitle('');
    setDescription('');
    setError(null);
  };

  return (
    <Well>
      <Flex direction="column" gap="size-150">
        <Flex direction="row" alignItems="center" gap="size-100">
          <Heading level={3}>➕ Add New Task</Heading>
        </Flex>
        
        <Divider size="S" />
        
        {error && (
          <View padding="size-100" backgroundColor="negative" borderRadius="small">
            <Text color="white" fontSize="size-75">
              {error}
            </Text>
          </View>
        )}
        
        <TextField
          label="Task Title"
          value={title}
          onChange={setTitle}
          placeholder="What needs to be done?"
          isRequired
          width="100%"
          UNSAFE_style={{ fontSize: '14px' }}
        />
        
        <TextArea
          label="Description (Optional)"
          value={description}
          onChange={setDescription}
          placeholder="Add more details about this task..."
          width="100%"
          rows={3}
          UNSAFE_style={{ fontSize: '14px' }}
        />
        
        <Flex direction="row" gap="size-100" justifyContent="flex-end">
          <Button
            variant="secondary"
            onPress={handleReset}
            isDisabled={isCreating}
          >
            Clear
          </Button>
          <Button
            variant="primary"
            onPress={handleSubmit}
            isDisabled={isCreating || !title.trim()}
          >
            {isCreating ? 'Creating...' : '✨ Create Task'}
          </Button>
        </Flex>
      </Flex>
    </Well>
  );
}


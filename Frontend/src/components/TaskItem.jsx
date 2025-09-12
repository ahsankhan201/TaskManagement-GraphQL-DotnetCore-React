import React, { useState } from 'react';
import {
  View,
  Flex,
  Text,
  Button,
  Badge,
  Divider,
} from '@adobe/react-spectrum';

export default function TaskItem({ task, onUpdateTaskStatus }) {
  const [isUpdating, setIsUpdating] = useState(false);
  const [error, setError] = useState(null);

  const handleStatusToggle = async () => {
    const newStatus = task.status === 'PENDING' ? 'COMPLETED' : 'PENDING';
    
    setIsUpdating(true);
    setError(null);
    
    try {
      await onUpdateTaskStatus(task.id, newStatus);
    } catch (err) {
      setError(err.message || 'Failed to update task status');
      console.error('Error updating task status:', err);
    } finally {
      setIsUpdating(false);
    }
  };

  const getStatusVariant = (status) => {
    return status === 'COMPLETED' ? 'success' : 'warning';
  };

  return (
    <View
      backgroundColor="gray-75"
      borderRadius="medium"
      padding="size-150"
      marginBottom="size-100"
      borderWidth="thin"
      borderColor="gray-300"
      UNSAFE_style={{
        opacity: task.status === 'COMPLETED' ? 0.8 : 1,
        transition: 'all 0.2s ease-in-out'
      }}
    >
      <Flex direction="column" gap="size-100">
        <Flex direction="row" justifyContent="space-between" alignItems="center">
          <Text 
            UNSAFE_style={{ 
              textDecoration: task.status === 'COMPLETED' ? 'line-through' : 'none',
              fontWeight: 'semibold',
              fontSize: '14px'
            }}
          >
            {task.status === 'COMPLETED' ? '✅' : '⏳'} {task.title}
          </Text>
          <Badge variant={getStatusVariant(task.status)}>
            {task.status}
          </Badge>
        </Flex>
        
        {task.description && (
          <Text 
            UNSAFE_style={{ 
              opacity: task.status === 'COMPLETED' ? 0.6 : 0.8,
              textDecoration: task.status === 'COMPLETED' ? 'line-through' : 'none',
              fontSize: '13px',
              lineHeight: '1.4'
            }}
          >
            {task.description}
          </Text>
        )}
        
        {error && (
          <View padding="size-75" backgroundColor="negative" borderRadius="small">
            <Text color="white" fontSize="size-75">
              {error}
            </Text>
          </View>
        )}
        
        <Divider size="S" />
        
        <Flex direction="row" justifyContent="space-between" alignItems="center">
          <Text fontSize="size-75" color="gray-600">
            Created: {new Date(task.createdAt).toLocaleDateString()}
          </Text>
          <Button
            variant="primary"
            onPress={handleStatusToggle}
            isDisabled={isUpdating}
            UNSAFE_style={{
              backgroundColor: task.status === 'PENDING' ? '#008800' : '#cc6600',
              fontSize: '12px',
              minWidth: '120px'
            }}
          >
            {isUpdating ? 'Updating...' : 
             task.status === 'PENDING' ? '✓ Complete' : '↩ Pending'}
          </Button>
        </Flex>
      </Flex>
    </View>
  );
}


import { useState, type FormEvent } from 'react'
import {
  updateTodo,
  deleteTodo,
  type Priority,
  type Todo,
} from '../services/todos'

type EditTodoModalProps = {
  todo: Todo
  onClose: () => void
  onTodoUpdated: (todo: Todo) => void
  onTodoDeleted: (id: number) => void
}

function EditTodoModal({
  todo,
  onClose,
  onTodoUpdated,
  onTodoDeleted,
}: EditTodoModalProps) {
  const [title, setTitle] = useState(todo.title)
  const [description, setDescription] = useState(todo.description ?? '')
  const [priority, setPriority] = useState<Priority>(todo.priority)
  const [dueDate, setDueDate] = useState(
    todo.dueDate ? todo.dueDate.split('T')[0] : ''
  )
  const [timeEstimate, setTimeEstimate] = useState(
    todo.timeEstimate?.toString() ?? ''
  )
  const [category, setCategory] = useState(todo.category ?? '')
  const [isSubmitting, setIsSubmitting] = useState(false)
  const [error, setError] = useState('')

  async function handleSubmit(event: FormEvent<HTMLFormElement>) {
    event.preventDefault()

    if (!title.trim()) {
      setError('Title is required.')
      return
    }

    try {
      setIsSubmitting(true)
      setError('')

      const updatedTodo = await updateTodo(todo.id, {
        title: title.trim(),
        description: description.trim() || null,
        dueDate: dueDate || null,
        priority,
        timeEstimate: timeEstimate ? Number(timeEstimate) : null,
        category: category.trim() || null,
        status: todo.status,
      })

      onTodoUpdated(updatedTodo)
      onClose()
    } catch (error) {
      console.error(error)
      setError('Could not update todo.')
    } finally {
      setIsSubmitting(false)
    }
  }

  async function handleDelete() {
    const confirmed = window.confirm(
      `Are you sure you want to delete "${todo.title}"?`
    )

    if (!confirmed) {
      return
    }

    try {
      setIsSubmitting(true)
      setError('')

      await deleteTodo(todo.id)

      onTodoDeleted(todo.id)
      onClose()
    } catch (error) {
      console.error(error)
      setError('Could not delete todo.')
    } finally {
      setIsSubmitting(false)
    }
  }

  return (
    <div className="modal-backdrop">
      <div className="todo-modal">
        <div className="todo-modal-header">
          <h2>Edit Todo</h2>

          <button
            type="button"
            className="modal-close-button"
            onClick={onClose}
          >
            ×
          </button>
        </div>

        <form onSubmit={handleSubmit}>
          <label>
            Title
            <input
              type="text"
              value={title}
              onChange={(event) => setTitle(event.target.value)}
              required
            />
          </label>

          <label>
            Description
            <textarea
              value={description}
              onChange={(event) => setDescription(event.target.value)}
            />
          </label>

          <label>
            Priority
            <select
              value={priority}
              onChange={(event) =>
                setPriority(event.target.value as Priority)
              }
            >
              <option value="Low">Low</option>
              <option value="Medium">Medium</option>
              <option value="High">High</option>
            </select>
          </label>

          <label>
            Due date
            <input
              type="date"
              value={dueDate}
              onChange={(event) => setDueDate(event.target.value)}
            />
          </label>

          <label>
            Time estimate (minutes)
            <input
              type="number"
              min="1"
              value={timeEstimate}
              onChange={(event) => setTimeEstimate(event.target.value)}
            />
          </label>

          <label>
            Category
            <input
              type="text"
              value={category}
              onChange={(event) => setCategory(event.target.value)}
            />
          </label>

          {error && <p className="form-error">{error}</p>}

          <div className="todo-modal-actions">
            <button
              type="button"
              className="delete-todo-button"
              onClick={handleDelete}
              disabled={isSubmitting}
            >
              Delete
            </button>

            <button
              type="button"
              onClick={onClose}
              disabled={isSubmitting}
            >
              Cancel
            </button>

            <button
              type="submit"
              disabled={isSubmitting}
            >
              {isSubmitting ? 'Saving...' : 'Save changes'}
            </button>
          </div>
        </form>
      </div>
    </div>
  )
}

export default EditTodoModal
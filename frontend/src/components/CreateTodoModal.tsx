import { useState, type FormEvent } from 'react'
import {
  createTodo,
  type Priority,
  type Todo,
} from '../services/todos'

type CreateTodoModalProps = {
  onClose: () => void
  onTodoCreated: (todo: Todo) => void
}

function CreateTodoModal({
  onClose,
  onTodoCreated,
}: CreateTodoModalProps) {
  const [title, setTitle] = useState('')
  const [description, setDescription] = useState('')
  const [priority, setPriority] = useState<Priority>('Medium')
  const [dueDate, setDueDate] = useState('')
  const [timeEstimate, setTimeEstimate] = useState('')
  const [category, setCategory] = useState('')
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

      const newTodo = await createTodo({
        title: title.trim(),
        description: description.trim() || null,
        dueDate: dueDate || null,
        priority,
        timeEstimate: timeEstimate ? Number(timeEstimate) : null,
        category: category.trim() || null,
      })

      onTodoCreated(newTodo)
      onClose()
    } catch (error) {
      console.error(error)
      setError('Could not create todo.')
    } finally {
      setIsSubmitting(false)
    }
  }

  return (
    <div className="modal-backdrop">
      <div className="todo-modal">
        <div className="todo-modal-header">
          <h2>Create Todo</h2>

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
              placeholder="What needs to be done?"
              required
            />
          </label>

          <label>
            Description
            <textarea
              value={description}
              onChange={(event) => setDescription(event.target.value)}
              placeholder="Add a description"
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
              placeholder="e.g. Work"
            />
          </label>

          {error && <p className="form-error">{error}</p>}

          <div className="todo-modal-actions">
            <button
              type="button"
              onClick={onClose}
            >
              Cancel
            </button>

            <button
              type="submit"
              disabled={isSubmitting}
            >
              {isSubmitting ? 'Creating...' : 'Create Todo'}
            </button>
          </div>
        </form>
      </div>
    </div>
  )
}

export default CreateTodoModal